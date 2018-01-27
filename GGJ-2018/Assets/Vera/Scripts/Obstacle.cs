using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    //[HideInInspector]
    public bool isTriggered;

    [Header ("Obstacle with animation")]
    public Animator myAnimator;
    public GameObject soundRay;

    [Header("All obstacles")]
    public AudioSource myAudioSource;
    public GameObject detectedParticle; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGround")
        {
            if (myAnimator != null)
            {
                myAnimator.SetBool("NextStep", true);
                Instantiate(soundRay, new Vector3(transform.position.x,transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    // public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "BackGround")
    //    {
    //        if (myAnimator != null)
    //        {
    //            myAnimator.SetBool("NextStep", true);
    //            Instantiate(soundRay, collision.contacts[0].point, Quaternion.identity);
    //            GetComponent<Rigidbody2D>().isKinematic = true;
    //        }
    //    }
    //}

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void AnimationEventTrigger()
    {
        print("test");
        isTriggered = false;
    }
}
