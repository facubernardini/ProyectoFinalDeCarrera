using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PruebaPlano : MonoBehaviour
{


    //public float murphyA, murphyB, murphyC;
    public Slider sliderA, sliderB, sliderC;


    // Start is called before the first frame update
    void Start()
    {

      
        

        
    }

    private void CalcularPlano(/*Vector3 a,Vector3 b, Vector3 c*/)
    {
        float murphyA = (float) (sliderA.value / 1.0f);
        float murphyB = (float)(sliderB.value / 1.0f);
        float murphyC = (float)(sliderC.value / 1.0f);

        Debug.Log(murphyA);

        Vector3 a2 = new Vector3(murphyA, 0, 0);
        Vector3 b2 = new Vector3(0, murphyB, 0);
        Vector3 c2 = new Vector3(0, 0, murphyC);
        
        if (murphyA == 0)
        {
            if (murphyB!=0)
                a2 = new Vector3(0.5f, murphyB, 0);
            else a2 = new Vector3(0.5f, 0 ,murphyC);
          
        }

        if (murphyB == 0)
        {
            if (murphyA != 0)
                b2 = new Vector3(murphyA, 0.5f, 0);
            else b2 = new Vector3( 0, 0.5f, murphyC);
       
        }

        if (murphyC == 0)
        {
            if (murphyA != 0)
                c2 = new Vector3(murphyA, 0, 0.5f);
            else c2 = new Vector3(0, murphyB, 0.5f);
     
        }


        Plane p = new Plane();
        p.Set3Points(a2, b2, c2);

        transform.up = p.normal;

        float distance = p.distance;

        transform.position = (transform.up * -distance) + new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //CalculateMurphy();
        CalcularPlano();
    }


    private void CalculateMurphy()
    {
       /* Vector3 a = new Vector3(0,0,0);
        Vector3 b = new Vector3(0,0,0);
        Vector3 c = new Vector3(0,0,0);

        if (murphyA != 0)
        {
            a = new Vector3(murphyA,0,0);
            if (murphyB != 0)
            {
                a.y = murphyB;
            }
            if (murphyC != 0)
            {
                a.z = murphyC;
            }
        }
        else
        {
            a.x = 0.1f;
            b.x = 0.2f;
            c.x = 0.3f;
        }

        if (murphyB != 0)
        {
            b = new Vector3(0,murphyB,0);
            if (murphyA != 0)
            {
                b.x = murphyB;
            }
            if (murphyC != 0)
            {
                b.z = murphyC;
            }
        }
        else
        {
            a.y = 0.1f;
            b.y = 0.2f;
            c.y = 0.3f;
        }

        if (murphyC != 0)
        {
            c=new Vector3(0,0,murphyC);
        }
        else
        {
            a.z = 0.1f;
            b.z = 0.2f;
            c.z = 0.3f;
        }

        CalcularPlano(a, b, c);*/
    }
}
