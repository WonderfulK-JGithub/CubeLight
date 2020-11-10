using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehavior : MonoBehaviour
{
    float distTraveled = 0;
    public float spd;

    public Vector3 dir;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(distTraveled <= 2)
        {
            transform.position += spd * dir;
            distTraveled += spd;
        }
    }
}
