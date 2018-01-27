using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour{
    public GameObject startCanvas,pauseCanvas;
    public GameObject quitMenu, optionMenu;
    public GameObject pointer;
    public GameObject dificultiLock;
    public GameObject gamOverScreen;

    void Start(){
        dificultiLock.SetActive(false);
        startCanvas.SetActive(true);
        quitMenu.SetActive(false);
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);
        gamOverScreen.SetActive(false);
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
        dificultiLock.SetActive(true);
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

    public void EndGameNo() {
        quitMenu.SetActive(false);
    }

    public void Highlight(Transform t){
        pointer.transform.SetParent(t);
        pointer.transform.localPosition = new Vector3(-75,0,0);
        pointer.SetActive(true);
    }

    public void noHighlight() {
        pointer.SetActive(false);
    }

    public void Restart() {
        SceneManager.LoadScene(1);
    }
}
