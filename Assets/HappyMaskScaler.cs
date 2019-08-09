using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyMaskScaler : MonoBehaviour {
    public HappyStateData happyState;
    public float maxSize = 4;
    public float happyTimeRemainingStart;

    void Start() {
        
    }
    void Update() {
        transform.localScale = Mathf.Clamp(happyState.happyTimeRemaining / happyTimeRemainingStart, 0, 1) * maxSize * Vector3.one;
    }
}
