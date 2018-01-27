using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRayController : MonoBehaviour 
{

    public static bool isFiringRay;

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

    [Header("Camera Shake")]
    public float shakeX;
    public float shakeY;
    public float shakeZ;
    public float shakeSpeed;
    public float shakeDuration;
    public float shakeRotate;

    private void Update()
    {
        RotateRay();

        if (Input.GetButtonDown("Jump") && Time.time >= rayRateCooldown)
        {
            rayRateCooldown = Time.time + rayCooldown;

            StartCoroutine(FireRay());
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
