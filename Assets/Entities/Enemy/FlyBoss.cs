using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBoss : MonoBehaviour
{
    private Enemy enemy;
    public GameObject smallFly;
    public GameObject objective;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.health <= 0)
        {
            GameObject child1 = Instantiate(smallFly);
            GameObject child2 = Instantiate(smallFly);
            GameObject child3 = Instantiate(smallFly);

            child1.transform.position = transform.position + new Vector3(-1, 1, 0);
            child2.transform.position = transform.position + new Vector3(-1, -1, 0);
            child3.transform.position = transform.position;

            GameObject obj = Instantiate(objective);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
