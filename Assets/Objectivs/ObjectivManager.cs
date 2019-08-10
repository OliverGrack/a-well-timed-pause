using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivManager : MonoBehaviour
{
    public BoolVar[] objectivs;

    private void Start() {
        foreach(var o in objectivs) {
            o.Reset();
        }
    }
}
