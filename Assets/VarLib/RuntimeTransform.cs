using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class RuntimeTransform : MonoBehaviour {
    public TransformVar variable;

    private void OnEnable() {
        variable.Value = transform;
    }

    private void OnDisable() {
        if (variable.Value == this) {
            variable.Value = null;
        }
    }
}
