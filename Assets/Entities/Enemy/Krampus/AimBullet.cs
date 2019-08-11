using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBullet : MonoBehaviour
{
    public TransformVar target;

    public float speed;

    public Transform explo;

    public float destroyAfterSec;

    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyAfterSec);
    }


    private void Update()
    {
        var force = (target.Value.position - transform.position);

        if (force.magnitude > 1 )
        {
            force = force.normalized;
        }

        rigid.AddForce(force * speed, ForceMode2D.Force);
    }

    private void OnDestroy()
    {
        Instantiate(explo, transform.position, Quaternion.identity);
    }
}
