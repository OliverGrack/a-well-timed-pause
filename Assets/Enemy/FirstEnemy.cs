using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstEnemy : MonoBehaviour {
    public HappyStateData happyState;
    public TransformVar player;
    public float speed;

    private Rigidbody rigid;

    private void Start() {
        rigid = GetComponent<Rigidbody>();
    }

    void Update() {
        if (player.Value == null) return;

        if (happyState.isSad) {
            rigid.AddForce(
                (player.Value.position - transform.position).normalized * speed,
                ForceMode.Force
            );
        }
    }
}
