using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//en class som håller all info i sparfilen
[System.Serializable]
public class SaveData 
{
    //bool array som innehåller information om man har klarat banan eller inte
    public bool[] levelData;

    //int array som innehåller information om vad det lägsta antalet clicks man har klarat banan med
    public int[] clickData;

    

    public SaveData (bool[] array, int[] clicks)
    {
        levelData = array;
        clickData = clicks;
    }
    
}


