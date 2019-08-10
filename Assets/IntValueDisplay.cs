using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class IntValueDisplay : MonoBehaviour
{
    private TMP_Text text;
    public IntVar value;
    public string formatter;
    public string singularFormatter;

    private int oldValue;

    private void Start() {
        text = GetComponent<TMP_Text>();
        updateText();
    }

    private void Update() {
        if (oldValue != value.Value) {
            updateText();
        }
    }

    private void updateText() {
        oldValue = value.Value;
        if (oldValue == 1) {
            text.text = singularFormatter.Replace("{}", oldValue.ToString());
        } else {
            text.text = formatter.Replace("{}", oldValue.ToString());
        }
    }

}
