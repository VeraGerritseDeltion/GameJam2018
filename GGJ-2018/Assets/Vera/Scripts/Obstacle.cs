using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [HideInInspector]
    public bool isTriggered;

    [Header ("Obstacle with animation")]
    public Collider2D myCollider;
    public Animator myAnimator;

    [Header("All obstacles")]
    public AudioSource myAudioSource;

    private void Start()
    {
        if(myCollider != null)
        {
            myCollider.enabled = false;
            StartCoroutine(Timer());
        }

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        myCollider.enabled = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.tag == "BackGround")
        {
            if(myAnimator != null)
            {
                myAnimator.SetBool("NextStep", true);
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
