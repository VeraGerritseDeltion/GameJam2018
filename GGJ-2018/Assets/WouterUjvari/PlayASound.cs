using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayASound : MonoBehaviour {
    public AudioSource myAudioSource;
    public AudioSource myAudioSource2;


    public void PlayASoundForMe(float pitch)
    {
        
        myAudioSource.pitch = pitch;
        myAudioSource.Play();
    }

    public void PlayASoundForMe2(float pitch)
    {

        myAudioSource2.pitch = pitch;
        myAudioSource2.Play();
    }
}
