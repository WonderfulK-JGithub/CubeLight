using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("FadeOut_TransitionEnter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
