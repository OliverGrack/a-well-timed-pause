using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class PostProcessingWeightByMood : MonoBehaviour {
    public PostProcessVolume volume;
    public HappyStateData happyState;
    public float startTime;
    public float maxTime;


    private void Update() {
        volume.weight = MathHelpers.Remap(happyState.HappyTimeSmoothed, startTime, maxTime, 0, 1);
    }
}
