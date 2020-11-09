using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCube : MonoBehaviour
{
    //referense till levelManagern
    LevelManager levelManager;

    //Coroutine variabel till för att spara en coroutine och sedan kolla om den är aktiv när man tänker starta en ny
    public Coroutine coroutine;

    //light referense
    Light cubeLight;
    
    //light prefab
    public GameObject lightObject;

    //public variabel för om kuben ska börja tänd eller inte
    public bool glowAtStart;

    //variabel för att sätta ljus
    float lightStrength;

    //variabel för om ljus ska gå ner eller inte
    bool lightDecrease;

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
        Circle,
        Triangle,
    }

    //variabel som berättar vilken form objektet har, 
    public Form form;

    void Start()
    {
        //Hämtar rätt lista baserad på vilken form man har gett den med "form" variabeln
        switch(form)
        {
            case Form.Cube:
                //hämtar listan
                cubeDirList = FindObjectOfType<DirectionManager>().cubeDirList;
                break;
            case Form.RotatedCube:
                //hämtar listan
                cubeDirList = FindObjectOfType<DirectionManager>().rotatedCubeDirList;
                break;
        }
        

        //skapar light och lägger referense i varaibel
        //Anledningen är för att jag vill kunna ändra ljusen som de ger ut utan att behöva gå egenom varenda kubs ljus
        GameObject newLight = Instantiate(lightObject, transform.position + Vector3.back, Quaternion.identity);
        cubeLight = newLight.GetComponent<Light>();

        //--gammalt--//Ger enabled rätt värde beroende på om man vill att kuben ska lysa på start eller inte
        //cubeLight.enabled = glowAtStart;

        if (glowAtStart) lightStrength = 1;
        else lightStrength = 0;

        cubeLight.intensity = lightStrength;

        //hämtar levelManager referense
        levelManager = FindObjectOfType<LevelManager>();

        lightDecrease = !glowAtStart;
    }

    //void som sker när man klickar på kuben
    private void OnMouseDown()
    {
        if(levelManager.clickAllow)
        {
            //--gammalt--//ger "enabled" på ljuset motsatt värde, så om ljuset är av sätts det på och om det är på sätts det av
            //cubeLight.enabled = !cubeLight.enabled;

            //Ser till att 2 av samma coroutine inte körs samtidigt
            if (coroutine != null) StopCoroutine(coroutine);

            //Startar Coroutinen som ändrar ljuset. Sparar Coroutine i en variabel
            coroutine = StartCoroutine(LightUp());


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


                    //Ser till att 2 av samma coroutine inte körs samtidigt
                    if (scriptReferense.coroutine != null) scriptReferense.StopCoroutine(scriptReferense.coroutine);

                    //Startar Coroutinen som ändrar ljuset. Sparar Coroutine i en variabel
                    scriptReferense.coroutine = scriptReferense.StartCoroutine(scriptReferense.LightUp());
                }
            }



            //skapar variabel som ska kolla om alla ljus är på
            bool checkLight = true;

            //foreach loop som går igenom alla kuber
            foreach (var item in FindObjectsOfType<ClickCube>())
            {
                //kollar om lightdecrease är true, om det stämmer är inte alla ljus tända och checkLight sätts till false
                if (item.lightDecrease) checkLight = false;
            }

            if (checkLight)
            {

                //Sätter leveln till completed
                FindObjectOfType<LevelComplete>().SetLevelComplete();

                //Startar Victory screenens animation, med en 2s delay
                FindObjectOfType<LevelComplete>().Invoke("StartTransition",2f);

                //Gör att man inte längre kan klicka på kuberna
                levelManager.clickAllow = false;

               

            }

        }


    }

    public IEnumerator LightUp()
    {
        
        //ger "lightDecrease" motsatt värde, så den slocknar om den lyser och lyser upp om den inte lyser.
        lightDecrease = !lightDecrease;

        if (!lightDecrease)
        {
            //ser till att coroutinen inte tar slut innan ljus styrkan är 100%
            while (lightStrength < 1)
            {
                //lägger till ljusstyrka
                lightStrength += lightSpeed;

                //ser till att ljusstyrkan inte råkar gå över 1
                if (lightStrength > 1) lightStrength = 1;

                //sätter ljusstyrkan på kubens ljus, samma värde som varaibeln som ökats
                cubeLight.intensity = lightStrength;

                //temporärt pausar coroutinen i 1 frame
                yield return null;
            }
        }
        else
        {
            //ser till att coroutinen inte tar slut innan ljus styrkan är 0%
            while (lightStrength > 0)
            {
                //tar bort ljusstyrka
                lightStrength -= lightSpeed;

                //ser till att ljusstyrkan inte kan råka gå under 0
                if (lightStrength < 0) lightStrength = 0;

                //sätter ljusstyrkan på kubens ljus, samma värde som varaibeln som minskat
                cubeLight.intensity = lightStrength;

                //temporärt pausar coroutinen i 1 frame
                yield return null;
            }
        }
        
    }

    
}
