using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour 
{

    private bool isInBossFight;

    public GameObject spiderBoss;

    public GameObject bossHpObject;
    public Image bossHpFill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isInBossFight)
            {
                isInBossFight = true;
                StartCoroutine(StartFight());
            }
        }
    }

    private IEnumerator StartFight()
    {
        BatController bat = GameObject.FindWithTag("Player").GetComponent<BatController>();
        float playerMoveSpeed = bat.moveSpeed;
        bat.moveSpeed = 0;

        Camera.main.GetComponent<CameraMovement>().target = spiderBoss.transform;

        yield return new WaitForSeconds(2);

        bossHpObject.SetActive(true);

        yield return new WaitForSeconds(1);

        Camera.main.GetComponent<CameraMovement>().target = bat.transform;
        bat.moveSpeed = playerMoveSpeed;
    }
}
