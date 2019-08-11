using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpider : MonoBehaviour
{
    private Enemy enemy;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.health <= 0)
        {
            float x = Random.Range(0.0f, 3.0f);
            if(x >= 2.0f)
            {
                GameObject i = Instantiate(item);
                i.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}
