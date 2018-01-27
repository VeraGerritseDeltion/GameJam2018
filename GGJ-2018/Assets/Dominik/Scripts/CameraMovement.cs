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

    public float deathZoomCamSize;
    public float deathZoomSpeed;

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
            if (!GameManager.instance.isDead)
            {
                transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * moveSpeed);
            }
            else
            {
                if (Camera.main.orthographicSize > deathZoomCamSize)
                {
                    Camera.main.orthographicSize -= (Time.deltaTime * deathZoomSpeed);
                }
            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, target.position + offset, Time.deltaTime * moveSpeed);
    }
}
