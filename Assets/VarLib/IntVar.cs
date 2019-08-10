using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVar : ScriptableObject {
    [SerializeField]
    private int value;

    public int Value { get => value; set => this.value = value; }

    public bool hasDefaultValue;
    public int defaultValue;

    public void Reset() {
        if (hasDefaultValue) {
            value = defaultValue;
        }
    }
}

