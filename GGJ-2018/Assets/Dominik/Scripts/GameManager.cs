using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

    public static GameManager instance;

    public enum GameState
    {
        Paused,
        Playing,
        Dead
    }
    public GameState gameState;

    [Space(10)]
    [Header("Health")]
    public int maxLives;
    private int currentLives;

    public GameObject heartObject;
    public Transform heartContainer;

    [Header("Bat Stuff")]
    public BatController batController;
    public Rigidbody2D batLetterRb;

    public Transform bossEntranceSpawnPoint;

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

    public IEnumerator StartGame(int lives)
    {
        maxLives = lives;

        yield return new WaitForSeconds(1f);

        BatRayController.canFire = true;

        for (int i = 0; i < maxLives; i++)
        {
            AddLive();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator GameOver()
    {
        gameState = GameState.Dead;

        batController.enabled = false;
        batController.gameObject.GetComponent<Collider2D>().enabled = false;
        batController.gameObject.GetComponent<Rigidbody2D>().simulated = false;

        CameraMovement camMove = Camera.main.GetComponent<CameraMovement>();
        camMove.transform.position = new Vector3(batController.transform.position.x, batController.transform.position.y, camMove.transform.position.z);

        while (Camera.main.orthographicSize > camMove.deathZoomCamSize)
        {
            yield return null;
        }

        batController.anim.SetTrigger("pDie");
        batLetterRb.simulated = true;

        yield return new WaitForSeconds(batController.anim.GetCurrentAnimatorStateInfo(0).length + 4f);
        UIManager.instance.gameOverPanel.SetActive(true);

        DataManager.instance.deaths++;
        UIManager.instance.deathCountText.text = "Death Count: " + DataManager.instance.deaths;
    }
}
