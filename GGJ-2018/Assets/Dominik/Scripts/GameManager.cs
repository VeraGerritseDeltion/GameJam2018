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

    [Header("Difficulty Settings")]
    public List<Animator> animatorsToChange = new List<Animator>();
    [Space(10)]
    public int easyHealth;
    public float easyRayWidthIncrease;
    public float easyObstacleAnimatorSpeed;
    [Space(10)]
    public int mediumHealth;
    public float mediumRayWidthIncrease;
    public float mediumObstacleAnimatorSpeed;
    [Space(10)]
    public int hardHealth;
    public float hardRayWidthIncrease;
    public float hardObstacleAnimatorSpeed;

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

    public void ApplyDifficultySettings()
    {
        float obstacleAnimatorSpeed = 1;

        switch (DataManager.instance.difficulty)
        {
            case DataManager.Difficulty.Easy:

                StartCoroutine(StartGame(easyHealth));
                GameObject.FindWithTag("Player").GetComponent<BatRayController>().batRayObject.GetComponent<BatRay>().scaleIncreaseSpeed = easyRayWidthIncrease;
                obstacleAnimatorSpeed = easyObstacleAnimatorSpeed;
                break;
            case DataManager.Difficulty.Medium:

                StartCoroutine(StartGame(mediumHealth));
                GameObject.FindWithTag("Player").GetComponent<BatRayController>().batRayObject.GetComponent<BatRay>().scaleIncreaseSpeed = mediumRayWidthIncrease;
                obstacleAnimatorSpeed = mediumObstacleAnimatorSpeed;
                break;
            case DataManager.Difficulty.Hard:

                StartCoroutine(StartGame(hardHealth));
                GameObject.FindWithTag("Player").GetComponent<BatRayController>().batRayObject.GetComponent<BatRay>().scaleIncreaseSpeed = hardRayWidthIncrease;
                obstacleAnimatorSpeed = hardObstacleAnimatorSpeed;
                break;
        }

        for (int i = 0; i < animatorsToChange.Count; i++)
        {
            animatorsToChange[i].speed = obstacleAnimatorSpeed;
        }
    }
}
