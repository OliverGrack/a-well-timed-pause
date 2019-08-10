using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntResetter : MonoBehaviour
{
    public IntVar[] vars;

    private void Start() {
        foreach(var v in vars) {
            v.Reset();
        }
    }
}
