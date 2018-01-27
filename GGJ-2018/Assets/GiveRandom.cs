using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveRandom : MonoBehaviour {

    public float rate;
    public float timer;
    public float chance;
    public Animator anim;
    public int test;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        test = Random.Range(0, 10);
        timer += Time.deltaTime;
        if(timer >= rate)
        {
            timer = 0;
            Randomize();
        }
    }

    void Randomize()
    {
        anim.SetInteger("Numbah", test);
    }
}
