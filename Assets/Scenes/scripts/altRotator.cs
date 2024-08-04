using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altRotator : MonoBehaviour
{

    public float speed = 10;
    private string sumbu;
    private bool putar;
    // Update is called once per frame
    void Update()
    {
        if(putar)
        {
            if(sumbu.ToUpper().Equals("X"))
            {
                transform.Rotate(Vector3.up * speed);
            } 
        }
    }

    public void RotateCube(string sumbu)
    {
        this.sumbu = sumbu;
        putar = true;
    }

    public void StopRotation( )
    {
        putar = false;
    }

   
}
