using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour 
{

    public static BossFight instance;

    private GameObject player;

    private bool isInBossFight;
    private bool canAggro;
    public bool isGrabbable;
    public bool grabbedPlayedRight;
    public bool grabbedPlayedLeft;

    public GameObject spiderBoss;
    public Animator bossAnim;

    public GameObject bossHpObject;
    public Image bossHpFill;

    public Transform spiderSecondCheckTriggerRight;
    public Transform spiderSecondCheckTriggerLeft;

    public float bossCamZoom;
    public float bossCamZoomSpeed;

    [Header("Camera Shake")]
    public float shakeX;
    public float shakeY;
    public float shakeZ;
    public float shakeSpeed;
    public float shakeDuration;
    public float shakeRotate;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (canAggro)
        {
            print("l");
            Vector3 offset = new Vector3(BossBattleManager.bsm.target.position.x - transform.localPosition.x, BossBattleManager.bsm.target.position.y - transform.localPosition.y, 0);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        }

        if (grabbedPlayedRight)
        {
            player.transform.position = spiderSecondCheckTriggerRight.position;
        }
        if (grabbedPlayedLeft)
        {
            player.transform.position = spiderSecondCheckTriggerLeft.position;
        }
    }

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
        canAggro = true;
    }

    public IEnumerator AttackPlayer()
    {
        BatController bat = player.GetComponent<BatController>();

        bossAnim.SetTrigger("pGrab");
        yield return new WaitForSeconds(0.75f);

        if (isGrabbable && bat.canGetHit)
        {
            if (Vector3.Distance(bat.transform.position, spiderSecondCheckTriggerRight.position) < Vector3.Distance(bat.transform.position, spiderSecondCheckTriggerLeft.position))
            {
                grabbedPlayedRight = true;
            }
            else
            {
                grabbedPlayedLeft = true;
            }
            bossAnim.SetTrigger("pPlayerCaught");
            bat.GetComponent<Rigidbody2D>().simulated = false;
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(3.5f);

        GameManager.instance.SubtractLive();
        Camera.main.transform.GetComponent<CameraShake>().Shake(shakeDuration, shakeX, shakeY, shakeZ, shakeRotate, shakeSpeed);
        StartCoroutine(bat.HitInvinsibility(2));

        yield return new WaitForSeconds(1f);

        grabbedPlayedRight = false;
        grabbedPlayedLeft = false;
        bat.GetComponent<Rigidbody2D>().simulated = true;
    }
}
