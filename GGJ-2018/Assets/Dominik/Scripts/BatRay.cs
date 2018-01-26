using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRay : MonoBehaviour 
{

    private Animator anim;
    private AudioSource audioSource;
    private List<Obstacle> triggeredObstacles;

    public float lifeTime;
    [Space(10)]
    public float moveSpeed;
    public float scaleIncreaseSpeed;
    public float volumeDecreaseSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.pitch = Random.Range(0.95f, 1.2f);
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
        audioSource.volume -= Time.deltaTime * volumeDecreaseSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Obstacle obstacle = collision.GetComponent<Obstacle>();

            if (!obstacle.isTriggered)
            {
                obstacle.isTriggered = true;
                triggeredObstacles.Add(obstacle);

                obstacle.myAnimator.SetTrigger("Highlight");
                obstacle.myAudioSource.Play();
            }
        }
    }

    private void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }

    private void AnimationEventDestroy()
    {
        for (int i = 0; i < triggeredObstacles.Count; i++)
        {
            triggeredObstacles[i].isTriggered = false;
        }

        Destroy(gameObject);
    }
}
