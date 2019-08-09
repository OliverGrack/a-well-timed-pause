using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position; // Initial offset from center
    }

    // LateUpdate is called once per frame after normal Update
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
