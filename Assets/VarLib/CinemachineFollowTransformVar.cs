using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineFollowTransformVar : MonoBehaviour {
    private CinemachineVirtualCamera cam;
    public TransformVar target;


    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.m_Follow = target.Value;
    }
}
