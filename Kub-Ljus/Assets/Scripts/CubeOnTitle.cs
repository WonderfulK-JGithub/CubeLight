using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeOnTitle : MonoBehaviour
{
    //referense till titel texten
    public Text text;

    //samma som kuberna i banorna, fast färre saker
    
    public Light cubeLight;

    public bool clickable = true;

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
        if(clickable)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(LightUp());
        }
       
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

                //ändrar alpha färg grej
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0.3f + (lightStrength / 5) * 1);

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

                text.color = new Color(text.color.r, text.color.g, text.color.b, 0.3f + (lightStrength / 5) * 1);

                yield return null;
            }


        }



    }


}

