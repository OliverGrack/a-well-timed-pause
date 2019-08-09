using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HappyStateData : ScriptableObject {
    public float happyTime;

    internal void Reset() {
        happyTime = 0;
    }

    public bool isHappy => happyTime > 0;
    public bool isSad => !isHappy;
}
