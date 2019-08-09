using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyStateManager : MonoBehaviour {
    public HappyStateData happyState;

    private void Start() {
        happyState.Reset();
    }

    void Update() {
        happyState.happyTime -= Time.deltaTime;
    }
}
