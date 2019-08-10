using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyMakerCollectable : MonoBehaviour
{
    public TransformVar player;
    public HappyStateData happyState;
    public float happyTime;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform == player.Value) {
            happyState.happyTime = Mathf.Max(happyState.happyTime, happyTime);
        }
    }
}
