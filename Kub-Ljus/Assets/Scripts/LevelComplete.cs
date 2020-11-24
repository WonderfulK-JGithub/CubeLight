using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    //referense till objektets animator
    Animator animator;

    //referenser till text som visar hur många gånger man klickat
    public Text clickText;
    public Text bestClicks;

    void Start()
    {
        //skaffa referensen
        animator = GetComponent<Animator>();
    }

    //accessas av knapp
    public void NextLevel()
    {
        //Hittar transition objektet och ber den att ladda nästa scene
        
        FadeTransition transition_objekt = FindObjectOfType<FadeTransition>();
        transition_objekt.StartCoroutine(transition_objekt.SlowSceneChange(SceneManager.GetActiveScene().buildIndex + 1));


        //spelar en ljudeffect
        AudioManager aManager = FindObjectOfType<AudioManager>();
        aManager.manager.PlayOneShot(aManager.clip1);
    }
    //accessas av knapp
    public void MainMenu()
    {
        //Hittar transition objektet och ber den att ladda första scenen (title screenen)

        FadeTransition transition_objekt = FindObjectOfType<FadeTransition>();
        transition_objekt.StartCoroutine(transition_objekt.SlowSceneChange(0));


        //spelar en ljudeffect
        AudioManager aManager = FindObjectOfType<AudioManager>();
        aManager.manager.PlayOneShot(aManager.clip1);
    }

    //callas när alla ljus är tända
    public void StartTransition()
    {
        //Startar rätt animation så att Victory screenen kommer fram
        animator.Play("Victory_turnAnimation");
    }

    public void SetLevelComplete()
    {
        foreach (var cube in FindObjectsOfType<ClickCube>())
        {
            cube.particles.Play();
        }


        //hämtar leveldata i en ny SaveData variabel
        SaveData data = SaveSystem.LoadLevelData();

        //kollar om datan är nu, dvs om filen inte existerade
        //Om den inte gör det görs det en ny bool aray som får alla värden till false
        if (data == null)
        {
           
            data = new SaveData(new bool[31],new int[31]);

        }

        //ger ett index för nuvarande scen
        int index = SceneManager.GetActiveScene().buildIndex;

        //gör rätt index i bool arayen inom SaveDatan till true, (true säger att banan är avklarad)
        data.levelData[index] = true;

        //hämtar antal klicks från levelmanagern
        int clickCount = FindObjectOfType<LevelManager>().amountOfClicks;

        

        //kollar om antal klicks nytt rekord, eller om rekordet inte har suttits och ligger på 0
        if(data.clickData[index] > clickCount || data.clickData[index] == 0)
        {
            data.clickData[index] = clickCount;
        }

        //Ger rätt text som ska visa hur mycket man har klickat
        clickText.text += clickCount.ToString();
        bestClicks.text += data.clickData[index];


        //Sparar den nya SaveDatan på en fil
        SaveSystem.SaveLevels(data.levelData,data.clickData);
        

        
    }
}
