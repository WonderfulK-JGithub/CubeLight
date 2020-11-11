﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    //referens till level selectens animator
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        
        
        //Hämtar leveldatan från en fil och sparar den i en SaveData variabel
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

        //skapar en variabel som refererar till bool arrayen i SaveDatan
        bool[] levelTrack = data.levelData;

        //Sätter det första värdet i levelTrack till true, eftersom Scene index 0 är titlescreenen och därför alltid räknas som avklarad (är den inte true är inte bana ett upplåst)
        levelTrack[0] = true;

        
        //foreach loop som går igenom alla knappar på level selecten
        foreach (var button in FindObjectsOfType<ButtonLevel>())
        {
            //Ger knappens isComplete värde samma värde som finns sparat
            button.isCompleted = levelTrack[button.sceneIndex];

           
            //kollar om knappen innan har ett scene index till en bana som är avklarad, om den har det blir knappens isUnlocked true
            if (levelTrack[button.sceneIndex - 1]) button.isUnlocked = true;

        }
        
    }

    public void ShowLevelSelect()
    {
        //spelar animationen som gör att levelSelect kommer fram
        animator.Play("LevelSelect_Rise");
    }

    public void ExitGame()
    {
        //avslutar spelet (om man har buildat det, funkar inte i editorn)
        Application.Quit();
    }
}