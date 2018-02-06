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

    private float defaultZoom;

    private void Awake()
    {
        offset = transform.position - target.position;
        defaultZoom = Camera.main.orthographicSize;
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
            if (GameManager.instance.gameState != GameManager.GameState.Dead)
            {
                transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * moveSpeed);

                if (BossFight.instance.grabbedPlayedLeft || BossFight.instance.grabbedPlayedRight)
                {
                    if (Camera.main.orthographicSize > BossFight.instance.bossCamZoom)
                    {
                        Camera.main.orthographicSize -= (Time.deltaTime * BossFight.instance.bossCamZoomSpeed);
                    }
                }
                else
                {
                    if (Camera.main.orthographicSize < defaultZoom)
                    {
                        Camera.main.orthographicSize += (Time.deltaTime * BossFight.instance.bossCamZoomSpeed);
                    }
                }
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
