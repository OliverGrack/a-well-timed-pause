﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int movementSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLookDirection();
        UpdateMovement();
    }

    void UpdateLookDirection()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void UpdateMovement()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal"), 0 , 0) * movementSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(0, Input.GetAxis("Vertical"), 0) * movementSpeed * Time.deltaTime;
    }
}
