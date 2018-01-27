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

        if (dropMenu.value == 0){
            difState = DifucultyState.Boooo;
        }
        else if (dropMenu.value == 1){
            difState = DifucultyState.Oke;
        }
        else{
            difState = DifucultyState.Yass;
        }

        switch (difState) {
            case DifucultyState.Boooo:
            GameManager.instance.maxLives = 5;
            StopCoroutine(GameManager.instance.StartGame());
            break;
            case DifucultyState.Oke:
            GameManager.instance.maxLives = 3;
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
