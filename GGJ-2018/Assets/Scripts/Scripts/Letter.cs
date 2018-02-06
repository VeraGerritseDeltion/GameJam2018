using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour {
    public AnimationControle animOther;

    public void StartAnim()
    {
        animOther.StartFlying();
    }
}
