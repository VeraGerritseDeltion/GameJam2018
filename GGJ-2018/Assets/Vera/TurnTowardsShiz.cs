using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardsShiz : MonoBehaviour {
    Vector3 mousePos;
    Vector3 target;
    float angle;


    private void Update()
    {
        mousePos = Input.mousePosition;
        target = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePos.x - target.x, mousePos.y - target.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
