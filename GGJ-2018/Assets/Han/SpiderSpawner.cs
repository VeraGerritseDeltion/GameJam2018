using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour{
    public GameObject spider;
    public float spawnTime;
    public int limit;
    int amount;

    private void Start() {
        StartCoroutine(spawnSpider());
    }

    IEnumerator spawnSpider(){
        yield  return new WaitForSeconds(spawnTime);
        amount++;
        if (amount <= limit){
            StartCoroutine(spawnSpider());
        }
        Instantiate(spider,transform.position,transform.rotation);
    }
}
