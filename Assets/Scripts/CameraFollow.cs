using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    public GameObject player;
    private void Start()
    {
        target = player.transform;
        offset = transform.position - target.position;
        player = GameObject.Find("Player");
    }

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, targetPos.z);
    }
}