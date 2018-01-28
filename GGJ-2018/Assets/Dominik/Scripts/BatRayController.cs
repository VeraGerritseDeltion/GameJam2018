using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRayController : MonoBehaviour 
{

    public static bool isFiringRay;

    public static bool canFire;

    public Animator anim;

    public Transform rayParent;
    public Transform newRaySpawn;
    [Space(10)]
    public float rotateSpeed;
    [Space(10)]
    public GameObject batRayObject;

    private float rayRateCooldown;
    public float rayCooldown;
    public int raysToFire;
    public float timeBetweenRays;

    Vector3 mousePos;
    Vector3 target;
    float angle;
    public Transform raySpawn;

    [Header("Camera Shake")]
    public float shakeX;
    public float shakeY;
    public float shakeZ;
    public float shakeSpeed;
    public float shakeDuration;
    public float shakeRotate;

    private void Update()
    {
        if (DataManager.instance.rayMovement == DataManager.RayMovement.Mouse)
        {
            RotateMouse();
        }
        else
        {
            RotateRay();
        }

        if (canFire)
        {
            if (Input.GetButtonDown("Jump") && Time.time >= rayRateCooldown || Input.GetButtonDown("Fire1") && Time.time >= rayRateCooldown)
            {
                rayRateCooldown = Time.time + rayCooldown;

                StartCoroutine(FireRay());
            }
        }
    }

    private void RotateMouse()
    {
        if (!isFiringRay)
        {
            mousePos = Input.mousePosition;
            target = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector2 offset = new Vector2(mousePos.x - target.x, mousePos.y - target.y);
            angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            raySpawn.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

    }

    private void RotateRay()
    {
        if (!isFiringRay)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rayParent.transform.Rotate(new Vector3(0, 0, -(Time.deltaTime * rotateSpeed)));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rayParent.transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));
            }
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private IEnumerator FireRay()
    {
        isFiringRay = true;

        anim.SetTrigger("pScreech");
        Camera.main.transform.GetComponent<CameraShake>().Shake(shakeDuration, shakeX, shakeY, shakeZ, shakeRotate, shakeSpeed);

        for (int i = 0; i < raysToFire; i++)
        {
            Instantiate(batRayObject, newRaySpawn.position, newRaySpawn.rotation);
            yield return new WaitForSeconds(timeBetweenRays);
        }

        isFiringRay = false;
    }
}
