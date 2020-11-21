using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOnTitle : MonoBehaviour
{
   


    //samma som kuberna i banorna, fast färre saker
    
    public Light cubeLight;

   

    float lightStrength;

    bool lightDecrease;

    public float lightSpeed;

    Coroutine coroutine;
    private void Start()
    {

        cubeLight.intensity = 0;
    }

    private void OnMouseDown()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(LightUp());
    }

    public IEnumerator LightUp()
    {
        
        lightDecrease = !lightDecrease;
        if (!lightDecrease)
        {
            
            while (lightStrength < 5)
            {               
                lightStrength += lightSpeed;
                
                if (lightStrength > 5) lightStrength = 5;
               
                cubeLight.intensity = lightStrength;
               
                yield return null;
            }
        }
        else
        {
            
            while (lightStrength > 0)
            {
              
                lightStrength -= lightSpeed;
                
                if (lightStrength < 0) lightStrength = 0;

                cubeLight.intensity = lightStrength;

                yield return null;
            }


        }



    }


}

