using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDrag : MonoBehaviour{
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, x *-10);
    }
}
