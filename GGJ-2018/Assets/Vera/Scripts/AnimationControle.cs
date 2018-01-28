using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControle : MonoBehaviour {
    public Camera cam;
    public Animator anim;
    public Camera mainCam;


    public void StartFlying()
    {
        anim = GetComponent<Animator>();
        transform.position = cam.transform.position;
        //mainCam.transform.position = transform.position;
        cam.enabled = false;
        anim.SetBool("StartFlying",true);
    }

    public void StopFlying()
    {
        anim.enabled = false;
        mainCam.transform.position = transform.position;
    }
}
