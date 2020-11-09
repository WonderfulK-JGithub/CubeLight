using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    //referense till objektets animator
    Animator animator;
    void Start()
    {
        //skaffa referensen
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    //accessas av knapp
    public void NextLevel()
    {
        //Hittar transition objektet och ber den att ladda nästa scene
        
        FadeTransition transition_objekt = FindObjectOfType<FadeTransition>();
        transition_objekt.StartCoroutine(transition_objekt.SlowSceneChange(SceneManager.GetActiveScene().buildIndex + 1));
    }
    //accessas av knapp
    public void MainMenu()
    {
        //Hittar transition objektet och ber den att ladda första scenen (title screenen)

        FadeTransition transition_objekt = FindObjectOfType<FadeTransition>();
        transition_objekt.StartCoroutine(transition_objekt.SlowSceneChange(0));
    }

    //callas när alla ljus är tända
    public void StartTransition()
    {
        //Startar rätt animation så att Victory screenen kommer fram
        animator.Play("Victory_turnAnimation");
    }

    public void SetLevelComplete()
    {
        
        //hämtar leveldata i en ny SaveData variabel
        SaveData data = SaveSystem.LoadLevelData();

        //kollar om datan är nu, dvs om filen inte existerade
        //Om den inte gör det görs det en ny bool aray som får alla värden till false
        if (data == null)
        {
            bool[] hej = new bool[31];
            for (int i = 0; i < hej.Length; i++)
            {
                hej[i] = false;
            }

            
            data = new SaveData(hej);
        }
        //gör rätt index i bool arayen inom SaveDatan till true, (true säger att banan är avklarad)
        data.levelData[SceneManager.GetActiveScene().buildIndex] = true;

        //Sparar den nya SaveDatan på en fil
        SaveSystem.SaveLevels(data.levelData);
        

        
    }
}
