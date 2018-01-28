using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {
    public float spiderSpeed;

    private void Update(){

        if (BossFight.instance.grabbedPlayedLeft == false && BossFight.instance.grabbedPlayedRight == false && GameManager.instance.isDead == false) {
            transform.Translate(-Vector3.up * Time.deltaTime * spiderSpeed);
        }
        Vector3 offset = new Vector3(BossBattleManager.bsm.target.position.x - transform.localPosition.x, BossBattleManager.bsm.target.position.y - transform.localPosition.y, 0);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    public void Movement(){

    }
}
