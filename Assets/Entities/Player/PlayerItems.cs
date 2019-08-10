using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public string pickupKey = "f";

    private Item itemInRange;

    private ArrayList inventory;

    public enum Items { Chocolate, Antidepressant, Wristband, Photo, Pizza };
    private PlayerCombatBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerCombatBehaviour>();
        inventory = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        Pickup();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        checkItem(col, false);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        checkItem(col, true);
    }

    void Pickup()
    {
        if(itemInRange != null && Input.GetKeyDown(pickupKey))
        {
            inventory.Add(itemInRange.item);
            itemInRange.Remove();
        }
    }

    void checkItem(Collider2D col, bool enter)
    {
        Item item = col.gameObject.GetComponent<Item>();
        if(player.alive && item != null)
        {
            item.SetInRange(enter);
            if(enter)
            {
                this.itemInRange = item;
            } else
            {
                this.itemInRange = null;
            }
        }
    }
}
