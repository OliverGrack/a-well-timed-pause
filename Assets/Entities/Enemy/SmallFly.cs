using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFly : MonoBehaviour
{
    private Enemy enemy;
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
            Destroy(gameObject);
        }
    }
}
