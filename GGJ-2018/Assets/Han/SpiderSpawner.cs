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
        if (amount <= limit){
            StartCoroutine(spawnSpider());
        }
        if(BossFight.instance.canAggro == true){
            amount++;
            Instantiate(spider, transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player"){
            BossFight.instance.bossHpFill.fillAmount -= 0.2f;
        }
    }
}
