using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public string pickupKey = "f";

    public string useItemKey = "q";
    public string switchItemKey = "tab";

    public HappyStateData happyState;
    private ItemData equippedItem;

    private Item itemInRange;

    private List<ItemData> inventory;

    private PlayerCombatBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerCombatBehaviour>();
        inventory = new List<ItemData>();
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
            ItemData pickedUp = itemInRange.item;
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
        else if(Input.GetKeyDown(useItemKey) && equippedItem != null)
        {
            Debug.Log("Consumed :" + equippedItem);
            int eqIndex = inventory.IndexOf(equippedItem);
            Consume(equippedItem);
            inventory.Remove(equippedItem);
            if(inventory.Count == 0)
            {
                equippedItem = null;
            } else
            {
                equippedItem = eqIndex == 0 ? inventory[0] : inventory[eqIndex - 1];
            }
            Debug.Log("Switched to: " + equippedItem);
        }
    }

    void Consume(ItemData item)
    {
        happyState.happyTime = Mathf.Max(happyState.happyTime, item.happyTime);
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

    ItemData getNextDifferentItem(int eqIndex)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            ItemData newItem = inventory[(i + eqIndex + 1) % inventory.Count];
            if (newItem != equippedItem)
            {
                return newItem;
            }
        }
        return equippedItem;
    }
}
