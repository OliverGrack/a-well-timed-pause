using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public HappyStateData happyState;
    public AudioSource happyMusic;
    public AudioSource sadMusic;

    public AnimationCurve sadMusicCurve;
    public AnimationCurve happyMusicCurve;

    public float happyMusicStart;
    public float happyMusicMax;

    public float sadMusicStart;
    public float sadMusicMax;

    void Update() {
        happyMusic.volume = happyMusicCurve.Evaluate(MathHelpers.Remap(happyState.HappyTimeSmoothed, happyMusicStart, happyMusicMax, 0, 1));
        sadMusic.volume = sadMusicCurve.Evaluate(MathHelpers.Remap(happyState.HappyTimeSmoothed, sadMusicStart, sadMusicMax, 0, 1));
    }
}
