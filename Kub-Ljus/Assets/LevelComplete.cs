using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //accessas av knapp
    public void NextLevel()
    {
        //laddar nästa scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //accessas av knapp
    public void MainMenu()
    {
        //laddar första scenen (title screenen)
        SceneManager.LoadScene(0);
    }
}
