using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthEatingDisplay : MonoBehaviour
{
    public GameObject normalMouths;
    public GameObject eatingMouth;
    public float eatingTime;
    public TransformVar player;
    private PlayerItems playerItems;

    private int lastNrOfPills = 0;

    private void Start() {
        normalMouths.SetActive(true);
        eatingMouth.SetActive(false);
    }

    void Update() {
        if (playerItems == null) {
            playerItems = player.Value.GetComponent<PlayerItems>();
        }

        var newCount = playerItems.inventory.Count;
        if (lastNrOfPills > newCount) {
            StopAllCoroutines();
            StartCoroutine(mouthChangeCooro());
        }
        lastNrOfPills = newCount;
    }

    IEnumerator mouthChangeCooro() {
        normalMouths.SetActive(false);
        eatingMouth.SetActive(true);
        yield return new WaitForSeconds(eatingTime);
        normalMouths.SetActive(true);
        eatingMouth.SetActive(false);
    }
}
