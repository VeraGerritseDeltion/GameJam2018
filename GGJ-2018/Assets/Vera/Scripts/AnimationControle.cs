using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControle : MonoBehaviour {
    public Camera cam;
    public Animator anim;
    public Camera mainCam;
    bool started;
    public Transform spawn;
    public UIManager stratugh;

    private void Awake()
    {
        if (DataManager.instance.hasStartedGameBefore)
        {
            cam.enabled = false;
            started = true;
            anim.enabled = false;
            StartCoroutine(Timer());

        }
        
    }


    public void StartFlying()
    {
        if (!started)
        {
            anim = GetComponent<Animator>();
            transform.position = cam.transform.position;
            //mainCam.transform.position = transform.position;
            cam.enabled = false;
            anim.SetBool("StartFlying",true);
        }

    }

    public void StopFlying()
    {
        anim.enabled = false;
        stratugh.StartGame();
        mainCam.transform.position = transform.position;

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 offset = mainCam.transform.position - transform.position;
        transform.position = spawn.position;

        mainCam.transform.position = spawn.position + offset;
    }
}
