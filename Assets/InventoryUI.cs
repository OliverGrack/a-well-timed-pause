using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public TransformVar player;
    private TMP_Text text;
    private PlayerItems playerItems;



    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update() {
        if (playerItems == null) {
            playerItems = player.Value.GetComponent<PlayerItems>();
        }

        text.text = playerItems.inventory.Count.ToString();
    }
}
