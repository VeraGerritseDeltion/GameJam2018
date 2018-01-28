using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {
    public float spiderSpeed;
    public BossBattleManager bsm;

    private void Update(){
        

        Vector3 offset = new Vector3(bsm.target.position.x - transform.localPosition.x, bsm.target.position.y - transform.localPosition.y,0);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        transform.Translate(-Vector3.up * Time.deltaTime * spiderSpeed);
    }

    public void Movement(){

    }
}
