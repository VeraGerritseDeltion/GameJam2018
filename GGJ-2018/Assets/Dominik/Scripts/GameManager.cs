using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

    public static GameManager instance;

    public int maxLives;
    private int currentLives;

    public GameObject heartObject;
    public Transform heartContainer;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddLive();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SubtractLive();
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
            GameOver();
        }
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < maxLives; i++)
        {
            AddLive();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GameOver()
    {
        print("Game Over!");
    }
}
