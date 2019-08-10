using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float expansionSpeed = 10.0f;
    public float range = 0.5f;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.up * expansionSpeed * Time.deltaTime;

        // check if it should be destroyed
        float deltaX2 = Mathf.Pow(transform.position.x - initPos.x, 2);
        float deltaY2 = Mathf.Pow(transform.position.y - initPos.y, 2);
        float delta = Mathf.Sqrt(deltaX2 + deltaY2);
        if (delta > range)
        {
            Destroy(gameObject);
        }
    }
}
