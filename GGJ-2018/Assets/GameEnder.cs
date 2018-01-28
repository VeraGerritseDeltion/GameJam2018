using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnder : MonoBehaviour {


    public void EndGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
