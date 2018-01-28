using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour{
    public GameObject spider;
    public float spawnTime;

    private void Start() {
        StartCoroutine(spawnSpider());
    }

    IEnumerator spawnSpider(){
        yield  return new WaitForSeconds(spawnTime);
        StartCoroutine(spawnSpider());
        Instantiate(spider,transform.position,transform.rotation);
    }
}
