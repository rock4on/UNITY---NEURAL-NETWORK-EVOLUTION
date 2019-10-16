using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai;
public class creier : MonoBehaviour
{
    public neuralnet n;
    public GameObject g;
    snezor s;
    public List<float> l = new List<float>();
    public mv m;
    public bool regenereaza = false;
    public bool mutate = false;
    public bool start=false;
    void Start()
    {
        n = new neuralnet();
        s = g.GetComponent<snezor>();
        m = g.GetComponent<mv>();
    }

    public void reset(neuralnet net)
    {
        start = true;
        n = new neuralnet(net);
        g.GetComponent<mv>().reset();
    }
      


    void Update()
    {
        
        if(regenereaza)
        {
            n = new neuralnet();
            n.genereaza(4, 5, 20, 10, 100);
            regenereaza = false;
        }

        if (mutate)
        {
            n.mutatie(1);
            mutate = false;
        }
        if (start)
        {
            l = new List<float>();
            l.Add(s.fdreapta);
            l.Add(s.fstanga);
            l.Add(s.fspate);
            l.Add(s.ffata);
            List<float> aux = n.proceseaza(l);
            m.rl = aux[0];
            m.rr = aux[1];
            m.mb = aux[2];
            m.mf = aux[3];
            m.ok = aux[4];
        }
    }
}
