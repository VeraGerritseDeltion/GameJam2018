using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRay : MonoBehaviour 
{

    private Animator anim;

    public float lifeTime;
    [Space(10)]
    public float moveSpeed;
    public float scaleIncreaseSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            FadeOut();
        }

        transform.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
        transform.localScale += new Vector3(Time.deltaTime * scaleIncreaseSpeed, Time.deltaTime * scaleIncreaseSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            collision.GetComponent<Animator>().SetTrigger("Highlight");
        }
    }

    private void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }

    private void AnimationEventDestroy()
    {
        Destroy(gameObject);
    }
}
