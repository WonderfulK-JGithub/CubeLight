using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehavior : MonoBehaviour
{
    //variabel som kollar hur långt objektet åkt
    float distTraveled = 0;

    //variabel för hur snabbt den åker
    public float spd;

    //variabel för åt vilket holl den åker
    public Vector3 dir;

    public bool dontDestroy = false;

    private void Start()
    {
        if (!dontDestroy)Invoke("dinMamma", 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(distTraveled <= 2)
        {
            transform.position += spd * dir;
            distTraveled += spd;
        }
        
    }
    void dinMamma()
    {
        Destroy(gameObject);
    }
}
