using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyStateManager : MonoBehaviour {
    public HappyStateData happyState;

    private void Start() {
        happyState.Reset();
    }

    void Update() {
        if (happyState.isHappy) {
            happyState.happyTimeRemaining = Mathf.Max(0, happyState.happyTimeRemaining - Time.deltaTime);
            happyState.timeSinceSad = 0;
        } else  {
            happyState.timeSinceSad += Time.deltaTime;
        }
    }
}
