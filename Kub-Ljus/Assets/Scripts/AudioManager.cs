using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource manager;

    public AudioClip clip1;
    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponent<AudioSource>();
    }
    
}
