using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{

    public enum Style
    {
        Clamped,
        Lerp
    }
    public Style style;

    private Vector3 offset;

    public Transform target;
    public float moveSpeed;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        if (style == Style.Clamped)
        {
            transform.position = target.position + offset;
        }
    }

    private void FixedUpdate()
    {
        if (style == Style.Lerp)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * moveSpeed);
        }
        //transform.position = Vector3.MoveTowards(transform.position, target.position + offset, Time.deltaTime * moveSpeed);
    }
}
