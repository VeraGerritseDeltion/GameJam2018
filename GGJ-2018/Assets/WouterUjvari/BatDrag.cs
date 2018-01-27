using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDrag : MonoBehaviour{
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if(y >= 0.1f)
        {
            y = 0;
        }
        this.gameObject.transform.rotation = Quaternion.Euler(y*40, 0, x *-10);
    }
}
