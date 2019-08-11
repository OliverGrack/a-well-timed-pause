using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int health;
    public float size;

    public Color hitColor;
    public float recolorTime;

    private SpriteRenderer[] renderers;
    private Color[] oriColors;

    // Start is called before the first frame update
    void Start() {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        oriColors = renderers.Select(r => r.color).ToArray();
    }

    IEnumerator recolorCoro() {
        foreach(var r in renderers) {
            r.color = hitColor;
        }
        yield return new WaitForSeconds(recolorTime);
        for (int i=0; i<renderers.Length; i++) {
            renderers[i].color = oriColors[i];
        }
    }

    public void ApplyDamage(int amount) {
        StopAllCoroutines();
        StartCoroutine(recolorCoro());


        health -= amount;
        if(health < 0)
        {
            health = 0;
        }
    }
}
