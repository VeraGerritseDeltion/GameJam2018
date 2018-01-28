using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScreen : MonoBehaviour
{

    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject quitMenu;
    public GameObject optionsMenu;
    //public GameObject pointer;
    public GameObject difficultyPanel;
    public GameObject gameOverPanel;

    public Dropdown difficultyDropdown;
    public Dropdown rayMovementDropdown;

    public TextMeshProUGUI deathCountText;

    void Start()
    {
        Time.timeScale = 0;

        if (DataManager.instance.restart && DataManager.instance.hasStartedGameBefore)
        {
            DataManager.instance.restart = false;

            startPanel.SetActive(false);
            difficultyPanel.SetActive(false);
            Time.timeScale = 1;

            switch (DataManager.instance.difficulty)
            {
                case DataManager.Difficulty.Easy:

                    StartCoroutine(GameManager.instance.StartGame(5));
                    break;
                case DataManager.Difficulty.Medium:

                    StartCoroutine(GameManager.instance.StartGame(3));
                    break;
                case DataManager.Difficulty.Hard:

                    StartCoroutine(GameManager.instance.StartGame(1));
                    break;
            }
        }
        else
        {
            startPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")){
            if (pausePanel.activeSelf == false && startPanel.activeSelf == false && difficultyPanel.activeSelf == false && optionsMenu.activeSelf == false){
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else if(quitMenu.activeSelf == true){
                quitMenu.SetActive(false);
                optionsMenu.SetActive(false);
            }
            else{
                pausePanel.SetActive(false);
                Time.timeScale = 1;
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
        Time.timeScale = 1;

        difficultyPanel.SetActive(false);

        switch (DataManager.instance.difficulty)
        {
            case DataManager.Difficulty.Easy:

                StartCoroutine(GameManager.instance.StartGame(5));
                break;
            case DataManager.Difficulty.Medium:

                StartCoroutine(GameManager.instance.StartGame(3));
                break;
            case DataManager.Difficulty.Hard:

                StartCoroutine(GameManager.instance.StartGame(1));
                break;
        }

        DataManager.instance.hasStartedGameBefore = true;
    }

    public void StartGame()
    {
        if (!DataManager.instance.hasStartedGameBefore)
        {
            startPanel.SetActive(false);
            difficultyPanel.SetActive(true);
            return;
        }

        Time.timeScale = 1;

        switch (DataManager.instance.difficulty)
        {
            case DataManager.Difficulty.Easy:

                StartCoroutine(GameManager.instance.StartGame(5));
                break;
            case DataManager.Difficulty.Medium:

                StartCoroutine(GameManager.instance.StartGame(3));
                break;
            case DataManager.Difficulty.Hard:

                StartCoroutine(GameManager.instance.StartGame(1));
                break;
        }

        startPanel.SetActive(false);

        DataManager.instance.hasStartedGameBefore = true;
    }

    public void EndGame()
    {
        pausePanel.SetActive(false);
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
        Time.timeScale = 1;

        pausePanel.SetActive(false);
        difficultyPanel.SetActive(false);
        optionsMenu.SetActive(false);
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
}
