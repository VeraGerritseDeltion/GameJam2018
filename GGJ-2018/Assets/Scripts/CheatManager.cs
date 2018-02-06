using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour 
{

    private void Update()
    {
        if (DataManager.instance.cheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                DataManager.instance.hasUsedCheatsThisRun = true;

                GameManager.instance.AddLive();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                DataManager.instance.hasUsedCheatsThisRun = true;

                GameManager.instance.SubtractLive();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                DataManager.instance.hasUsedCheatsThisRun = true;

                GameObject.FindGameObjectWithTag("Player").transform.position = GameManager.instance.bossEntranceSpawnPoint.position;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                DataManager.instance.hasUsedCheatsThisRun = true;

                StartCoroutine(BossFight.instance.KillBoss());
            }
        }
    }
}
