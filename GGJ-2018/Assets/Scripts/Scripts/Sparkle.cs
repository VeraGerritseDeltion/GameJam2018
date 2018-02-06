using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour {
    Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        SparkleRand();
    }

    void SparkleRand()
    {
        StartCoroutine(Timer(Random.Range(2, 10)));        
    }

    IEnumerator Timer(int randomSpark)
    {
        yield return new WaitForSeconds(randomSpark);
        myAnim.SetTrigger("Spark");
        SparkleRand();
    }
}
