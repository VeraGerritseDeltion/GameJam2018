using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour{
    enum DifucultyState{
        Boooo,
        Oke,
        Yass
    }

    DifucultyState difState;
    Dropdown dropMenu;

    void Start() {
        difState = DifucultyState.Boooo;
    }

    public void ChangeDificulty(){
        //difState = dropMenu.value;

        switch (difState) {
            case DifucultyState.Boooo:
            GameManager.instance.maxLives = 5;
            StopCoroutine(GameManager.instance.StartGame());
            break;
            case DifucultyState.Oke:
            GameManager.instance.maxLives = 2;
            StopCoroutine(GameManager.instance.StartGame());
            break;
            case DifucultyState.Yass:
            GameManager.instance.maxLives = 1;
            StopCoroutine(GameManager.instance.StartGame());
            break;
            default:
            break;
        }


    }

}
