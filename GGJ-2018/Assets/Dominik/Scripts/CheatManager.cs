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
                GameManager.instance.AddLive();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                GameManager.instance.SubtractLive();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = GameManager.instance.bossEntranceSpawnPoint.position;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                StartCoroutine(BossFight.instance.KillBoss());
            }
        }
    }
}
