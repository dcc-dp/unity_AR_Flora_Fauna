using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatorAqui : MonoBehaviour
{

    public float speed = 10;
    private string sumbu;
    private bool putar;
    // Update is called once per frame
    void Update()
    {
        if(putar)
        {
            if (sumbu.ToUpper().Equals("X"))
            {
                transform.Rotate(Vector3.forward * speed );
            }
            else if (sumbu.ToUpper().Equals("Y"))
            {
                transform.Rotate(Vector3.up * speed );
            }
            else if (sumbu.ToUpper().Equals("Z"))
            {
                transform.Rotate(Vector3.forward * speed);
            }
        }
    }

    public void RotateCube(string sumbu)
    {
        Debug.Log(sumbu);
        this.sumbu = sumbu;
        putar = true;
    }

    public void StopRotation( )
    {
        putar = false;
    }

   
}
