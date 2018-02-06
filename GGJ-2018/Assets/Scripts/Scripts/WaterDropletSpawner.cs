using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropletSpawner : MonoBehaviour {

    public float spawnRate;
    public GameObject droplet;
    public Transform spawnLoc;

	void Start () {
        Spawn();
	}

    void Spawn()
    {
        StartCoroutine(SpawnTimer());
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnRate);
        Spawn();
        Instantiate(droplet, spawnLoc.position, Quaternion.identity);
    }

}
