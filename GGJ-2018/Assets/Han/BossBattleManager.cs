using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleManager : MonoBehaviour {
    public static BossBattleManager bsm;
    public Transform target;

    private void Start() {
        bsm = this;
    }
}
