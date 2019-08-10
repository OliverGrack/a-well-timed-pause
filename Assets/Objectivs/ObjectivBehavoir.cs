using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivBehavoir : MonoBehaviour {
    public TransformVar player;
    public BoolVar hasObjectiv;

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

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        if (other.transform == player) {
            hasObjectiv.Value = true;
        }
    }
}
