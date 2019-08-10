using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public string pickupKey = "f";

    public string useItemKey = "q";
    public string switchItemKey = "tab";

    public HappyStateData happyState;
    private Items equippedItem;

    private Item itemInRange;

    private ArrayList inventory;

    public enum Items { Chocolate, Antidepressant, Wristband, Photo, Pizza, None };
    private PlayerCombatBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerCombatBehaviour>();
        inventory = new ArrayList();
        equippedItem = Items.None;
    }

    // Update is called once per frame
    void Update()
    {
        Pickup();
        InventoryInput();
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
            Items pickedUp = itemInRange.item;
            inventory.Add(pickedUp);
            itemInRange.Remove();
            equippedItem = pickedUp;
            Debug.Log("Picked up: " + pickedUp);
        }
    }

    void InventoryInput()
    {
        if(Input.GetKeyDown(switchItemKey) && inventory.Count > 1)
        {
            int eqIndex = inventory.IndexOf(equippedItem);
            equippedItem = getNextDifferentItem(eqIndex);
            Debug.Log("Switched to: " + equippedItem);
        }
        else if(Input.GetKeyDown(useItemKey) && equippedItem != Items.None)
        {
            Debug.Log("Consumed :" + equippedItem);
            int eqIndex = inventory.IndexOf(equippedItem);
            Consume(equippedItem);
            inventory.Remove(equippedItem);
            if(inventory.Count == 0)
            {
                equippedItem = Items.None;
            } else
            {
                equippedItem = eqIndex == 0 ? 0 : (Items)inventory[eqIndex - 1];
            }
            Debug.Log("Switched to: " + equippedItem);
        }
    }

    void Consume(Items item)
    {
        switch(item)
        {
            case Items.Antidepressant:
                happyState.happyTime = Mathf.Max(happyState.happyTime, 10f);
                break;
            case Items.Chocolate:
                happyState.happyTime = Mathf.Max(happyState.happyTime, 5f);
                break;
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

    Items getNextDifferentItem(int eqIndex)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Items newItem = (Items) inventory[(i + eqIndex + 1) % inventory.Count];
            if (newItem != equippedItem)
            {
                return newItem;
            }
        }
        return equippedItem;
    }
}
