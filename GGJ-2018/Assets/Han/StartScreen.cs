using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour{
    public GameObject startCanvas,pauseCanvas;
    public GameObject quitMenu, optionMenu;

    void Start(){
        quitMenu.SetActive(false);
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update(){
        if (Input.GetButtonDown("Cancel")){
            if(pauseCanvas.activeSelf == false && startCanvas.activeSelf == false){
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else if(quitMenu.activeSelf == true || optionMenu.activeSelf == true){
                quitMenu.SetActive(false);
                optionMenu.SetActive(false);
            }
            else{
                pauseCanvas.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }


    public void StartGame(){
        startCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndGame(){
        quitMenu.SetActive(true);
    }

    public void OptionMenu(){
        optionMenu.SetActive(true);
    }

    public void EndGameYes(){
        Application.Quit();
    }

    public void EndGameNo(){
        quitMenu.SetActive(false);
    }
}
