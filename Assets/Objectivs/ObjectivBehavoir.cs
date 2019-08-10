using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ObjectivBehavoir : MonoBehaviour {
    public TransformVar player;
    public BoolVar hasObjectiv;
    public CinemachineImpulseSource impulseSource;
    public float impulseStrength;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        anim.SetBool("HasObjectiv", hasObjectiv.Value);
        if (hasObjectiv.Value == true) {
            Destroy(gameObject, 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision);
        if (collision.transform == player.Value) {
            hasObjectiv.Value = true;
            impulseSource.GenerateImpulse(Vector3.one * impulseStrength);
        }
    }
}
