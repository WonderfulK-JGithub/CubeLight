using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    //variabel för vilken scene knappen ska ladda
    public int sceneIndex;

    //variabel för om banan som knappen representerar är avklarad
    public bool isCompleted;

    //variabel för om banan som knappen representerar är upplåst
    public bool isUnlocked;

    //3 färg variabler för de olika stadium knapparna kan vara i, dvs låst, upplåst eller avklarad
    public Color lockedColor;
    public Color unlockedColor;
    public Color completedColor;

    //referense till knappens bild (public eftersom scriptet inte ligger på knappen utan knappens text)
    public Image buttonImage;

    private void Start()
    {
        //Ger knappen samma siffra som level den kommer ladda
        GetComponent<Text>().text = (sceneIndex).ToString();
    }

    //sker när man trycker på knappen
    public void LoadLevel()
    {
        if(isUnlocked)
        {
            //laddar rätt scene
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private void Update()
    {

        //if satts som ser till att knapparna har rätt färg beroende på om banan som de representerar är låst, upplåst eller avklarad
        if(isCompleted)
        {
            buttonImage.color = completedColor;
        }
        else if(isUnlocked)
        {
            buttonImage.color = unlockedColor;
        }
        else
        {
            buttonImage.color = lockedColor;
        }
    }
}
