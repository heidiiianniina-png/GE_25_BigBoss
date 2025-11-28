using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioActivator : MonoBehaviour
{     
    public AudioSource audioSourceInThisObject;
    public KeyCode keyCodeForActivation;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceInThisObject = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCodeForActivation))
        {
            if (audioSourceInThisObject.isPlaying)
            {
                audioSourceInThisObject.Pause();
            }
            else
            {             
                audioSourceInThisObject.Play();
            }
        }
    }
}
