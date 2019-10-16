using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mv : MonoBehaviour
{
    public Vector3 startpoz;
    public float speed = 30;
    public float rl;
    public float rr;
    public float mb;
    public float mf;
    public int fcd = 500;
    public GameObject Text;
    int cd = 0;
    float rotation = 0;
    public float ok = 0;
    Rigidbody2D rig;
    public int hp=100;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
       // startpoz = transform.position;
    }

    public void reset()
    {
        transform.position = startpoz;
        transform.rotation=  Quaternion.identity;
    }

    

    public void shoot(Vector3 v)
    {
        
        GetComponent<Collider2D>().enabled = false;
        RaycastHit2D hitsp = Physics2D.Raycast(transform.position, v);
        GetComponent<Collider2D>().enabled = true;
        if (hitsp.collider.CompareTag("bul"))
        {
            hitsp.collider.gameObject.GetComponent<mv>().hp-=10;

        }
        DrawLine(transform.position, v*hitsp.distance, Color.green, 1f);
    }

    
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        
         lr.SetColors(color,color);
        lr.SetWidth(0.01f, 0.01f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }




    void Update()
    {
        float f = transform.rotation.ToEulerAngles().z;
        float sinf = Mathf.Sin(f), cosf = Mathf.Cos(f);
        Vector2 fata = new Vector2(-sinf, cosf);
        Text.GetComponent<TextMesh>().text = hp.ToString();




        rig.velocity = Vector2.zero;
        rig.freezeRotation = true;
        
        //if (Input.GetAxis("Horizontal") > 0) rr = 1;
        //else if (Input.GetAxis("Horizontal") < 0) rl = 1;

        //if (Input.GetAxis("Vertical") > 0) mf = 1;
        //else if (Input.GetAxis("Vertical") < 0) mb = 1;



        if (rl > 0.5f)
        {
            rig.rotation += 1f;
            //  rig.rotation = rotation;
            rl = 0;

        }
        if (rr > 0.5f)
        {
            rig.rotation -= 1f;
            //rig.rotation = rotation;
            rr = 0;
        }
        if (mb > 0.5f)
        {
            //rig.AddForce(2*Vector2.down);
            mb = 0;
            rig.AddRelativeForce(speed * Vector2.down);

        }
        if (mf > 0.5f)
        {
            rig.AddRelativeForce(speed * Vector2.up);
            mf = 0;
        }

        /*if (Input.GetButtonDown("Jump")) ok = true;*/
        if (cd==0 &&ok>0.5f)
        {
            cd = fcd;
            shoot(fata);
        }
        if (cd > 0) cd--;
    }
       
}

