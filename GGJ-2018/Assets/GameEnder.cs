using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameEnder : MonoBehaviour
{

    public TextMeshProUGUI finishedGameText;

    private void Start()
    {
        finishedGameText.text = DataManager.instance.finishedGameMessage;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
