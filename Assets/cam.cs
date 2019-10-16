using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public int poz=0;
    public int maxpoz=9;
    void Start()
    {
        
    }

    public void mv()
    {
        poz++;
        if (poz == maxpoz) poz = 0;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
