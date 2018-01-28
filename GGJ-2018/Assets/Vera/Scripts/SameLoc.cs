using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameLoc : MonoBehaviour {
    public Transform toFollow;

	void Update () {
        transform.position = new Vector3(toFollow.position.x, toFollow.position.y, -60);
	}
}
