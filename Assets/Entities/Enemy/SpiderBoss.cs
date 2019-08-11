using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : MonoBehaviour
{
    private Enemy enemy;
    public GameObject smallSpider;
    public GameObject objective;

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
            GameObject child1 = Instantiate(smallSpider);
            GameObject child2 = Instantiate(smallSpider);
            GameObject child3 = Instantiate(smallSpider);

            child1.transform.position = child1.transform.position + new Vector3(1, 0, 0);


            Instantiate(objective);
            Destroy(gameObject);
        }
    }
}
