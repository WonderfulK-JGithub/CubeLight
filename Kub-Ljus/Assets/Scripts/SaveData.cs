using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//en class som håller all info i sparfilen
[System.Serializable]
public class SaveData 
{
    //bool aray som berättar om varje level är klarad eller inte
    public bool[] levelData;

    public SaveData (bool[] array)
    {
        levelData = array;
    }
    
}
