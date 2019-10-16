using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snezor : MonoBehaviour
{
    public float dist = 0;
    Collider2D c;
    public float ffata=0;
    public float fspate = 0;
    public float fstanga = 0;
    public float fdreapta = 0;
    void Start()
    {
        c = GetComponent<Collider2D>();
    }

    

    // Update is called once per frame
    void Update()
    {
        float f= transform.rotation.ToEulerAngles().z;
        float sinf= Mathf.Sin(f), cosf= Mathf.Cos(f);
        Vector2 spate = new Vector2(sinf, -cosf);
        Vector2 fata = new Vector2(-sinf, cosf);
        Vector2 stanga = new Vector2(-cosf,-sinf);
        Vector2 dreapta = new Vector2(cosf, sinf);
        c.enabled = false;
        RaycastHit2D hitsp = Physics2D.Raycast(transform.position , spate);
        RaycastHit2D hitf = Physics2D.Raycast(transform.position, fata);
        RaycastHit2D hitst = Physics2D.Raycast(transform.position, stanga);
        RaycastHit2D hitd = Physics2D.Raycast(transform.position, dreapta);
        ffata = hitf.distance;
        fspate = hitsp.distance;
        fstanga = hitst.distance;
        fdreapta = hitd.distance;
        if (hitsp.collider != null)
        {
         //   Debug.Log("s:"+hitsp.distance.ToString()+" f:"+hitf.distance.ToString()+"st: "+hitst.distance.ToString() + "dr: "+ hitd.distance.ToString());
        }
        c.enabled = true;
    }
}
