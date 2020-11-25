using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    //animator referense
    public Animator animator;

    void Awake()
    {
        //Startar animationen för att transitiona in
        animator.Play("Transition_Enter");
    }

    //coroutine till för att ändra scene när skärmen är svart
    public IEnumerator SlowSceneChange(int sceneIndex)
    {
        //Startar animationen för att transitiona ut
        animator.Play("Transition_Exit");

        //While loop som väntar på att animationen tagit slut och sedan bytt till Transition_Start
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_Start") )
        {
            yield return null;
        }

        //Laddar rätt scene
        SceneManager.LoadScene(sceneIndex);

    }

    public void Delete()
    {
        SaveSystem.Delete();
    }

    
}
