using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ai
{
    class node
    {

        public List<float> conexiuni;
        public float valoare = 0;
        public float greutate = 0;

        private float sigmoid(float f)
        {
            f = 1 / (1 + (float)Math.Exp(-f));
            return f;
        }


        public node()
            {
                conexiuni = new List<float>();
            }

        void citeste(List<float> aux)
        {
            conexiuni = aux;
        }

        void schimba(int poz,float valoare)
        {
            conexiuni[poz] = valoare;
        }

        public void proceseaza(List<float> aux)
        {
            valoare = 0;
            for(int i=0;i<aux.Count;i++)
            {
                valoare += aux[i] * conexiuni[i];
            }
            valoare = sigmoid(valoare+greutate);
        }


    }

    class layer
    {
        public List<node> nodes= new List<node>();
        public int size; 
        

        public List<float> valori()
        {
            List<float> aux=new List<float>();
            foreach(node n in nodes)
            {
                aux.Add(n.valoare);
            }
            return aux;
        }

        public void schimba(layer l)
        {
            nodes = l.nodes;
            size = l.size;
        }


        public void schimbaval(List<float> n)
        {
            for(int i=0;i<size;i++)
            {
                nodes[i].valoare = n[i];
            }
        }



    }


   public class neuralnet
    {
        int input_size;
        int size;
        List<layer> layers=new List<layer>();

        public neuralnet()
        {
            
        }



        public static neuralnet cross(neuralnet a,neuralnet b,int nr_cat)
        {
            neuralnet aux = new neuralnet(a);
            int piv = (a.layers.Count + b.layers.Count) / (2*nr_cat);
            

            for (int i = piv; i < b.size; i++)
            {
                if (i > aux.size) aux.layers.Add(new layer());
                aux.layers[i].schimba(b.layers[i]);
            }

            foreach(node n in aux.layers[piv].nodes)
            {
                while (n.conexiuni.Count > aux.layers[piv - 1].size)
                {
                    n.conexiuni.RemoveAt(n.conexiuni.Count);

                }
                while(n.conexiuni.Count< aux.layers[piv-1].size)
                {
                    n.conexiuni.Add(Random.Range(-100f, 100f));
                }

            }


            return aux;
        }



        public neuralnet (neuralnet n)
        {
            this.input_size = n.input_size;
            this.layers = n.layers;
            this.size = n.size;
        }
        public void citire(string s)
        {
            try
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    input_size = int.Parse(sr.ReadLine());
                    size = int.Parse(sr.ReadLine());
                    layers.Add(new layer());
                    layers[0].size = input_size;
                    for (int i = 0; i < input_size; i++)
                    {
                        layers[0].nodes.Add(new node());
                    }

                    for(int i=1;i<size;i++)
                    {
                        layers.Add(new layer());
                        layers[i].size = int.Parse(sr.ReadLine());
                        for(int j=0;j< layers[i].size;j++)
                        {
                            layers[i].nodes.Add(new node());
                            layers[i].nodes[j].greutate = float.Parse(sr.ReadLine());

                                for (int k = 0; k < layers[i - 1].size; k++)
                            {
                                layers[i].nodes[j].conexiuni.Add(float.Parse(sr.ReadLine()));
                            }
                        }
                    }
                    
                }


            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
               
            }
        }

        public void export(string s)
        {

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(s))
            {
                       


                file.WriteLine(input_size.ToString());
                file.WriteLine(size.ToString());
                
            

                for (int i = 1; i < size; i++)
                {
                    file.WriteLine(layers[i].size);
                    for (int j = 0; j < layers[i].size; j++)
                    {

                        file.WriteLine(layers[i].nodes[j].greutate);

                        for (int k = 0; k < layers[i - 1].size; k++)
                        {
                            file.WriteLine(layers[i].nodes[j].conexiuni[k].ToString());
                        }
                    }
                }


        

            }


        }

            
         //public float rfloat(float minimum, float maximum)
         //   {
         //       float f= (float)r.NextDouble() * (maximum - minimum) + minimum;
         //       return f;
         //   }

        List<float> gen_f(int s)
        {
            List<float> f = new List<float>();
            for(int i=0;i<s;i++)
            {
                f.Add(Random.Range(-100f,100f));
            }

            return f;
        }

        public void mutatie(int nr_mutatii)
        {
            while (nr_mutatii > 0)
            {
                int tip = Random.Range(0, 3);
                switch (tip)
                {
                    case 0:
                        int k = Random.Range(1, size - 1);
                        layers[k].nodes.Add(new node());
                        layers[k].size++;
                        layers[k].nodes[layers[k].size - 1].conexiuni = gen_f(layers[k - 1].size);
                        layers[k].nodes[layers[k].size - 1].greutate = Random.Range(-100f, 100f);
                        foreach (node n in layers[k+1].nodes)
                        {
                            n.conexiuni.Add(Random.Range(-100f, 100f));
                                
                        }

                        break;
                    case 1:
                        int l = Random.Range(1, size - 1);
                        int j = Random.Range(1, layers[l].size);

                        layers[l].nodes[j].greutate = Random.Range(-100f, 100f);

                        break;
                    case 2:
                        int p = Random.Range(1, size - 1);
                        int q = Random.Range(1, layers[p].size);
                        int i = Random.Range(1, layers[p].nodes[q].conexiuni.Count);
                        layers[p].nodes[q].conexiuni[i]=Random.Range(-100f,100f);
                            
                        break;


                }
                nr_mutatii--;
            }
        }





        public void genereaza(int ins,int ous, int s, int npl, float w)
        {
            input_size = ins;
            size=s;

            layers.Add(new layer());
            layers[0].size = input_size;
            for (int i = 0; i < input_size; i++)
            {
                layers[0].nodes.Add(new node());
            }
            

           

            for (int i = 1; i < size-1; i++)
            {
                layers.Add(new layer());
                layers[i].size = Random.Range(1, npl);
                for (int j = 0; j < layers[i].size; j++)
                {
                    layers[i].nodes.Add(new node());
                    layers[i].nodes[j].greutate = Random.Range(-w,w);

                    for (int k = 0; k < layers[i - 1].size; k++)
                    {
                        if(Random.Range(0,1)>0)
                        layers[i].nodes[j].conexiuni.Add(Random.Range(-w,w));
                        else
                        layers[i].nodes[j].conexiuni.Add(0);
                    }
                }
            }

            layers.Add(new layer());
            layers[size - 1].size = ous;

            for (int j = 0; j < layers[size-1].size; j++)
            {
                layers[size-1].nodes.Add(new node());
                layers[size-1].nodes[j].greutate = Random.Range(-w, w);

                for (int k = 0; k < layers[size-1 - 1].size; k++)
                {
                    layers[size-1].nodes[j].conexiuni.Add(Random.Range(-w, w));
                }
            }


        }











        public List<float> proceseaza(List<float> f)
        {
            layers[0].schimbaval(f);
            for(int i=1;i<size;i++)
            {
                foreach(node n in layers[i].nodes)
                {
                    n.proceseaza(layers[i - 1].valori());
                }
            }
            List<float> l = new List<float>();
            foreach(node n in layers[size-1].nodes)
            {
                l.Add(n.valoare);
            }
            return l;
        }


    }
}
