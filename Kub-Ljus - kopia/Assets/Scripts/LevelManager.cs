using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    

    //variabel som ser till att man bara kan trycka på kuberna när denna variabel är true
    public bool clickAllow = true;

    //varaibel som kollar hur många gånger man klickar
    [HideInInspector]
    public int amountOfClicks = 0;

    

}

