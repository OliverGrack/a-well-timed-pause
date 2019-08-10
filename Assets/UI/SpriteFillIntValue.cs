using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFillIntValue : MonoBehaviour
{
    public IntVar value;
    public int maxValue;

    private Image img;

    private void Start() {
        img = GetComponent<Image>();
    }

    private void Update() {
        img.fillAmount = value.Value / (float)maxValue;
    }
}
