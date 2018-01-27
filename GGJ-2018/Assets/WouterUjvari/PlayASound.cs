using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayASound : MonoBehaviour {
    public AudioSource myAudioSource;


    public void PlayASoundForMe(float pitch)
    {
        
        myAudioSource.pitch = pitch;
        myAudioSource.Play();
    }
}
