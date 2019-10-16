using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bul : MonoBehaviour
{
    public bool is_active=false;
    int nr_framse=150;
    
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        
            Destroy(collision.gameObject);
        


    }


    public void incepe(Vector3 v)
    {
        Rigidbody2D r = GetComponent<Rigidbody2D>();
        is_active = true;
        r.AddForce(v);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (is_active)
        {
            if (nr_framse < 0) Destroy(this.gameObject);
            nr_framse--;
        }
    }
}
