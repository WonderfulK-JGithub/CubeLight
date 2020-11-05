using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCube : MonoBehaviour
{
    //light referense
    Light cubeLight;
    
    //light prefab
    public GameObject lightObject;

    //public variabel för om kuben ska börja tänd eller inte
    public bool glowAtStart;

    //variabel för att sätta ljus
    float lightStrength;

    //variabel för hur snabbt ljuset ändras
    public float lightSpeed;

    //Lista på alla håll som kuben ska skicka raycast i när den klickas på, se nedan vid OnMouseDown
    //Hämtar listan från en manager, för jag vill kunna ändra listan i unity editorn utan att behöva ändra för varje kub
    List<Vector3> cubeDirList;


    //Enum för olika former

    
    public enum Form
    {
        Cube,
        RotatedCube,
    }

    //variabel som berättar vilken form objektet har, 
    public Form form;

    void Start()
    {
        
        //hämtar listan
        cubeDirList = FindObjectOfType<DirectionManager>().cubeDirList;

        //skapar light och lägger referense i varaibel
        //Anledningen är för att jag vill kunna ändra ljusen som de ger ut utan att behöva gå egenom varenda kubs ljus
        GameObject newLight = Instantiate(lightObject, transform.position + Vector3.back, Quaternion.identity);
        cubeLight = newLight.GetComponent<Light>();

        //--gammalt--//Ger enabled rätt värde beroende på om man vill att kuben ska lysa på start eller inte
        //cubeLight.enabled = glowAtStart;

        if (glowAtStart) lightStrength = 1;
        else lightStrength = 0;

        cubeLight.intensity = lightStrength;
    }

    //void som sker när man klickar på kuben
    private void OnMouseDown()
    {
        //--gammalt--//ger "enabled" på ljuset motsatt värde, så om ljuset är av sätts det på och om det är på sätts det av
        //cubeLight.enabled = !cubeLight.enabled;

        //Startar Coroutinen som ändrar ljuset
        StartCoroutine(LightUp());

        //foreach loop som går igenom varje håll som kuben ska skicka raykasts åt
        foreach (var direction in cubeDirList)
        {
            //variabel som sparar raycast träff
            RaycastHit hit;

            //Raycast åt ett av hållen i listan, om den träffar en annan kub sker koden nedanför
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                //skaffar referense för scriptet på kuben
                ClickCube scriptReferense = hit.collider.GetComponent<ClickCube>();

                //--gammalt--//Får kubens light.enabled motsatt värde, exakt som tidigare
                //scriptReferense.cubeLight.enabled = !scriptReferense.cubeLight.enabled;

                //Startar Coroutinen som ändrar ljuset
                scriptReferense.StartCoroutine(scriptReferense.LightUp());
            }
        }
    }

    public IEnumerator LightUp()
    {
        print("hej");
        if (lightStrength == 0)
        {
            while (lightStrength < 1)
            {
                lightStrength += lightSpeed;

                if (lightStrength > 1) lightStrength = 1;

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

    private void Update()
    {
        print(lightStrength);
    }
}
