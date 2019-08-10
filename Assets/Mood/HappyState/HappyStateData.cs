using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HappyStateData : ScriptableObject {
    public float happyTime;

    [SerializeField]
    private float happyTimeSmoothed;
    public float HappyTimeSmoothed => happyTimeSmoothed;
    [SerializeField]
    private float smoothTime;
    private float smoothVelocity;

    public void Reset() {
        happyTime = 0;
        happyTimeSmoothed = 0;
        smoothVelocity = 0;
    }

    public void Update() {
        happyTimeSmoothed = Mathf.SmoothDamp(happyTimeSmoothed, happyTime, ref smoothVelocity, smoothTime);
    }

    public bool isHappy => happyTime > 0;
    public bool isSad => !isHappy;
}
