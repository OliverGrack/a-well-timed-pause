using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool inRange;
    public PlayerItems.Items item;

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInRange(bool isInRange)
    {
        this.inRange = isInRange;
        if (isInRange)
        {
            sprite.color = Color.yellow;
        } else
        {
            sprite.color = Color.cyan;
        }
        // Show pick up key ("[F] to pick up") 
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
