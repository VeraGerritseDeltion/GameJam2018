using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour 
{

    public static BossFight instance;

    private GameObject player;

    public bool isInEatRange;
    private bool isInBossFight;
    public bool canAggro;
    public bool isGrabbable;
    public bool grabbedPlayedRight;
    public bool grabbedPlayedLeft;
    public AudioSource bossmusic;
    public AudioSource normalmusic;

    public GameObject spiderBossObject;
    public GameObject spiderBoss;
    public Animator bossAnim;

    public GameObject bossHpObject;
    public Image bossHpFill;

    public Transform spiderRotate;
    public Transform spiderSecondCheckTriggerRight;
    public Transform spiderSecondCheckTriggerLeft;

    public float bossCamZoom;
    public float bossCamZoomSpeed;

    public Animator victoryAnim1;
    public Animator victoryAnim2;

    public List<AudioClip> spiderSounds = new List<AudioClip>();
    public AudioSource audioSource;

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
            BatController bat = GameObject.FindWithTag("Player").GetComponent<BatController>();
            if (bat.GetComponent<Rigidbody2D>().simulated == true)
            {
                Vector3 offset = new Vector3(BossBattleManager.bsm.target.position.x - spiderRotate.localPosition.x, BossBattleManager.bsm.target.position.y - spiderRotate.localPosition.y, 0);
                float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                spiderRotate.rotation = Quaternion.Euler(0, 0, angle - 90);
            }

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

        audioSource.clip = spiderSounds[Random.Range(0, spiderSounds.Count)];
        audioSource.Play();
        bossmusic.Play();
        normalmusic.Stop();
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

            audioSource.clip = spiderSounds[Random.Range(0, spiderSounds.Count)];
            audioSource.Play();

            bossAnim.SetTrigger("pPlayerCaught");
            bossAnim.SetBool("bEat", true);
            bat.GetComponent<Rigidbody2D>().simulated = false;
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(3.5f);

        audioSource.clip = spiderSounds[Random.Range(0, spiderSounds.Count)];
        audioSource.Play();

        GameManager.instance.SubtractLive();
        Camera.main.transform.GetComponent<CameraShake>().Shake(shakeDuration, shakeX, shakeY, shakeZ, shakeRotate, shakeSpeed);
        StartCoroutine(bat.HitInvinsibility(2));

        yield return new WaitForSeconds(1f);

        grabbedPlayedRight = false;
        grabbedPlayedLeft = false;

        bossAnim.SetBool("bEat", false);
        bat.GetComponent<Rigidbody2D>().simulated = true;

        if (isInEatRange)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    public IEnumerator KillBoss()
    {
        yield return new WaitForSeconds(1);

        BatController bat = player.GetComponent<BatController>();
        bat.moveSpeed = 0;

        Camera.main.GetComponent<CameraMovement>().target = spiderBoss.transform;

        yield return new WaitForSeconds(2);

        spiderBossObject.SetActive(false);

        yield return new WaitForSeconds(1);

        Camera.main.GetComponent<CameraMovement>().target = bat.transform;

        UIManager.instance.fadeScreenAnim.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene("Ending");
    }
}
