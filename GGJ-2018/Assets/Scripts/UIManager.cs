using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    private bool restartLevelOnContinue;

    [Header("Panels")]
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject quitMenu;
    public GameObject optionsMenu;
    public GameObject difficultyPanel;
    public GameObject gameOverPanel;
    public GameObject cheatsText;

    [Header("Dropdowns")]
    public Dropdown difficultyDropdown;
    public Dropdown rayMovementDropdown;
    public Dropdown changeDifficultyDropdown;
    public Dropdown cheatDropdown;

    [Header("Text")]
    public TextMeshProUGUI deathCountText;

    [Header("Animators")]
    public Animator fadeScreenAnim;

    int lives;

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

    private void Start()
    {
        DataManager.instance.hasUsedCheatsThisRun = false;

        Time.timeScale = 0;

        if (DataManager.instance.restart && DataManager.instance.hasStartedGameBefore)
        {
            DataManager.instance.restart = false;

            startPanel.SetActive(false);
            difficultyPanel.SetActive(false);

            GameManager.instance.gameState = GameManager.GameState.Playing;
            Time.timeScale = 1;

            GameManager.instance.ApplyDifficultySettings();
        }
        else
        {
            startPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameManager.instance.gameState == GameManager.GameState.Playing)
            {
                GameManager.instance.gameState = GameManager.GameState.Paused;

                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else if (GameManager.instance.gameState == GameManager.GameState.Paused)
            {
                if (quitMenu.activeInHierarchy)
                {
                    quitMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                }

                if (pausePanel.activeInHierarchy)
                {
                    GameManager.instance.gameState = GameManager.GameState.Playing;

                    pausePanel.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }

        if (startPanel.activeInHierarchy || 
            pausePanel.activeInHierarchy || 
            quitMenu.activeInHierarchy || 
            optionsMenu.activeInHierarchy || 
            difficultyPanel.activeInHierarchy || 
            gameOverPanel.activeInHierarchy)
        {
            BatRayController.canFire = false;
        }
        else
        {
            BatRayController.canFire = true;
        }
    }

    public void StartGameButtonWithChosentDifficulty()
    {
        SetDifficulty();

        GameManager.instance.gameState = GameManager.GameState.Playing;
        Time.timeScale = 1;

        difficultyPanel.SetActive(false);
        DataManager.instance.hasStartedGameBefore = true;
    }

    public void StartMyGame()
    {
        GameManager.instance.gameState = GameManager.GameState.Playing;

        GameManager.instance.ApplyDifficultySettings();
    }

    public void StartGame()
    {
        if (!DataManager.instance.hasStartedGameBefore)
        {
            startPanel.SetActive(false);
            difficultyPanel.SetActive(true);
            return;
        }

        GameManager.instance.gameState = GameManager.GameState.Playing;

        Time.timeScale = 1;

        GameManager.instance.ApplyDifficultySettings();

        startPanel.SetActive(false);

        DataManager.instance.hasStartedGameBefore = true;
    }

    public void EndGame()
    {
        pausePanel.SetActive(false);

        if (GameManager.instance.gameState == GameManager.GameState.Dead)
        {
            gameOverPanel.SetActive(false);
        }

        quitMenu.SetActive(true);
    }

    public void OptionMenu()
    {
        pausePanel.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void EndGameYes()
    {
        Application.Quit();
    }

    public void EndGameNo()
    {
        Time.timeScale = 1;
        quitMenu.SetActive(false);

        if (GameManager.instance.gameState == GameManager.GameState.Dead)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        DataManager.instance.restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetDifficulty()
    {
        switch (difficultyDropdown.value)
        {
            case 0:

                DataManager.instance.difficulty = DataManager.Difficulty.Easy;
                break;
            case 1:

                DataManager.instance.difficulty = DataManager.Difficulty.Medium;
                break;
            case 2:

                DataManager.instance.difficulty = DataManager.Difficulty.Hard;
                break;
        }
    }

    public void ContinueButton()
    {
        if (restartLevelOnContinue)
        {
            Restart();
        }
        else
        {
            GameManager.instance.gameState = GameManager.GameState.Playing;
            Time.timeScale = 1;

            pausePanel.SetActive(false);
            difficultyPanel.SetActive(false);
            optionsMenu.SetActive(false);
        }

    }

    public void SetRayMovement()
    {
        switch (rayMovementDropdown.value)
        {
            case 0:

                DataManager.instance.rayMovement = DataManager.RayMovement.Mouse;
                break;

            case 1:

                DataManager.instance.rayMovement = DataManager.RayMovement.Keys;
                break;
        }
    }

    public void SetCheatDropdown()
    {
        switch (cheatDropdown.value)
        {
            case 0:

                DataManager.instance.cheatsEnabled = false;
                cheatsText.SetActive(false);
                break;
            case 1:

                DataManager.instance.cheatsEnabled = true;
                cheatsText.SetActive(true);
                break;
        }
    }

    public void SetNewDifficultyDropdown()
    {
        int currentDifficulty = (int)DataManager.instance.difficulty;

        switch (changeDifficultyDropdown.value)
        {
            case 0:

                DataManager.instance.difficulty = DataManager.Difficulty.Easy;
                break;
            case 1:

                DataManager.instance.difficulty = DataManager.Difficulty.Medium;
                break;
            case 2:

                DataManager.instance.difficulty = DataManager.Difficulty.Hard;
                break;
        }

        if ((int)DataManager.instance.difficulty != currentDifficulty)
        {
            restartLevelOnContinue = true;
        }
    }
}
