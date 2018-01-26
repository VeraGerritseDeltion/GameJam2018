using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private static bool shake;

    private Vector3 startPos;
    private Vector3 startRot;

    private float shakeTime;

    // Variables to edit.

    // Shake speed.
    private float shakeSpeed;
    // Shake amount on the X axis.
    private float shakeAmountX;
    // Shake amount on the Y axis.
    private float shakeAmountY;
    // Shake amount on the Z axis.
    private float shakeAmountZ;
    // Rotate amount
    private float rotateAmount;

    private void Update()
    {
        if (shake)
        {
            Vector3 nextPos = new Vector3(Random.Range(startPos.x - shakeAmountX, startPos.x + shakeAmountX), 
                                          Random.Range(startPos.y - shakeAmountY, startPos.y + shakeAmountY), 
                                          Random.Range(startPos.z - shakeAmountZ, startPos.z + shakeAmountZ));

            Quaternion nextRot = Quaternion.Euler(new Vector3(Random.Range(startRot.x - rotateAmount, startRot.x + rotateAmount),
                                                              Random.Range(startRot.y - rotateAmount, startRot.y + rotateAmount),
                                                              Random.Range(startRot.z - rotateAmount, startRot.z + rotateAmount)));

            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, nextPos, Time.deltaTime * shakeSpeed);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, nextRot, Time.deltaTime * shakeSpeed);

            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
            }
            else
            {
                Camera.main.transform.position = startPos;
                Camera.main.transform.rotation = Quaternion.Euler(startRot);
                shake = false;
            }
        }
        else
        {
            shakeTime = 0;
        }
    }

    public void Shake(float _duration, float _shakeAmountX, float _shakeAmountY, float _shakeAmountZ, float _rotateAmount, float _shakeSpeed)
    {
        if (!shake)
        {
            startPos = Camera.main.transform.position;
            startRot = Camera.main.transform.rotation.eulerAngles;

            shakeAmountX = _shakeAmountX;
            shakeAmountY = _shakeAmountY;
            shakeAmountZ = _shakeAmountZ;

            rotateAmount = _rotateAmount;
            shakeSpeed = _shakeSpeed;

            shakeTime += _duration;
            shake = true;
        }
    }
}
