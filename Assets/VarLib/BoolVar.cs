using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolVar : ScriptableObject {
    [SerializeField]
    private bool value;

    public bool Value { get => value; set => this.value = value; }

    public bool hasDefaultValue;
    public bool defaultValue;

    public void Reset() {
        if (hasDefaultValue) {
            value = defaultValue;
        }
    }
}

