using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

    public static GameManager instance;

    [HideInInspector]
    public bool isDead;
    public int maxLives;
    public StartScreen sc;
    private int currentLives;

    public GameObject heartObject;
    public Transform heartContainer;

    public BatController batController;
    public Rigidbody2D batLetterRb;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddLive();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SubtractLive();
        }
    }

    public void AddLive()
    {
        if (currentLives < maxLives)
        {
            Instantiate(heartObject, heartContainer);
            currentLives++;
        }
    }

    public void SubtractLive()
    {
        if (currentLives > 0)
        {
            heartContainer.GetChild((heartContainer.childCount - 1)).GetComponent<Animator>().SetTrigger("Destroy");
            currentLives--;
        }

        if (currentLives <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < maxLives; i++)
        {
            AddLive();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator GameOver()
    {
        batController.enabled = false;
        batController.gameObject.GetComponent<Collider2D>().enabled = false;
        batController.gameObject.GetComponent<Rigidbody2D>().simulated = false;

        isDead = true;

        CameraMovement camMove = Camera.main.GetComponent<CameraMovement>();
        camMove.moveSpeed = 100f;

        while (Camera.main.orthographicSize > camMove.deathZoomCamSize)
        {
            yield return null;
        }

        batController.anim.SetTrigger("pDie");
        batLetterRb.simulated = true;

        yield return new WaitForSeconds(batController.anim.GetCurrentAnimatorStateInfo(0).length + 4f);
        sc.gamOverScreen.SetActive(true);
        
    }
}
