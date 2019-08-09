using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HappyStateData : ScriptableObject {
    public float timeSinceSad;

    public float happyTimeRemaining;

    internal void Reset() {
        timeSinceSad = 0;
        happyTimeRemaining = 0;
    }

    public bool isHappy => happyTimeRemaining > 0;
    public bool isSad => !isHappy;
}
