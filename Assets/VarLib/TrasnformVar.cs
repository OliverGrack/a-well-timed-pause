using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TrasnformVar : ScriptableObject {
    private Transform value;

    public Transform Value { get => value; set => this.value = value; }
}
