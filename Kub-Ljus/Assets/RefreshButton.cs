using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RefreshButton : MonoBehaviour
{
    public void Refresh()
    {
        //Hittar transition objektet och ber den att ladda om scenen
        FadeTransition transition_objekt = FindObjectOfType<FadeTransition>();
        transition_objekt.StartCoroutine(transition_objekt.SlowSceneChange(SceneManager.GetActiveScene().buildIndex));

        
    }
}
