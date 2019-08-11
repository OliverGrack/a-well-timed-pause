using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBasedShow : MonoBehaviour
{
    public HappyStateData happyState;
    public int max;
    public int min;

    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.enabled = (
                happyState.HappyTimeSmoothed >= min &&
                happyState.HappyTimeSmoothed < max
        );
    }
}
