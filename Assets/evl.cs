using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai;
public class evl : MonoBehaviour
{
    public GameObject Timer;
    public GameObject obj1;
    public GameObject obj2;
   
    float time=0;
    creier c1;
    creier c2;
   public int i = 0, j = 0;
    List<neuralnet> n;
    List<float> nval;
    public int parent1 = 0;
    public int parent2 = 1;
    public int gen = 0;
    public int size = 10;
    public int nsize = 5;
    public int npl = 3;
    public float w = 1000;
    public int nr = 0;
    public bool export = false;
    void Start()
    {
        c1 = obj1.GetComponent<creier>();
        c2 = obj2.GetComponent<creier>();
        
        nval = new List<float>();
        n = new List<neuralnet>();
        for(int i=0;i<size;i++)
        {
            nval.Add(0);
            n.Add(new neuralnet());
            n[i].genereaza(4,5,nsize,npl,w);
        }
       
    }

    void newgen()
    {

        nr++;
        for (int k = 0; k < size; k++)
            for (int l = 0; l < size; l++)
            {
                if (nval[k] < nval[l])
                {
                    float aux = nval[k];
                    nval[k] = nval[l];
                    nval[l] = aux;
                    neuralnet a = new neuralnet(n[k]);
                    n[k] = new neuralnet(n[l]);
                    n[l] = new neuralnet(n[k]);
                }

            }

        

        for(int k=4;k<size-2;k++)
        {
            if(k%2==0)
            {
                n[k] = new neuralnet(neuralnet.cross(n[parent1], n[parent2], 2));
            }
            else
            {
                n[k] = new neuralnet(neuralnet.cross(n[parent2], n[parent1], 2));
            }
        }

        for(int k=size-2;k<size;i++)
        n[k].genereaza(4, 5, nsize, npl, w);

        for(int k=0;k<size;k++)
        {
            nval[k] = 0;
        }
        gen++;
    }


    void Update()
    {
        if (export)
        {
            n[0].export("C:/Users/andre/OneDrive/Desktop/nodeboss.txt");
            export = false;
        }

        Timer.GetComponent<TextMesh>().text=time.ToString();
        if (gen == 0)
        {
            c1.reset(n[i]);
            c2.reset(n[j]);
            gen++;
        }
        if(time>30)
        {
            nval[i] = nval[i] + c1.m.hp-c2.m.hp;
            nval[j] = nval[j] + c2.m.hp -c1.m.hp;
            j++;
            if (j > size - 1)
            {
                j = i; i++;
                if (i > size - 1) { i = 0;j = 0;  newgen();  }
                
            }
            c1.reset(n[i]);
            c2.reset(n[j]);
            time = 0;
        }
        time += Time.fixedDeltaTime;
    }
}
