using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour 
{

    public static DataManager instance;

    public bool hasStartedGameBefore;
    public bool restart;
    public bool completedTutorial;
    public bool cheatsEnabled;
    public bool hasUsedCheatsThisRun;

    public int deaths;

    public string finishedGameMessage;

    public enum RayMovement
    {
        Mouse,
        Keys
    }
    public RayMovement rayMovement;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public Difficulty difficulty;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void SetupFinishedGameMessage()
    {
        string difficultyText = "";

        switch (difficulty)
        {
            case Difficulty.Easy:

                difficultyText = "<color=green>easy</color>";
                break;
            case Difficulty.Medium:

                difficultyText = "<color=orange>medium</color>";
                break;
            case Difficulty.Hard:

                difficultyText = "<color=red>hard</color>";
                break;
        }

        if (!hasUsedCheatsThisRun)
        {
            finishedGameMessage = "You beat the game on " + difficultyText + "\nwithout using cheats!";
        }
        else
        {
            finishedGameMessage = "You beat the game on " + difficultyText + "\nbut you used cheats.. \nshame on you";
        }
    }
}
