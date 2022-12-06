using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinisLine : MonoBehaviour
{
    [SerializeField] private Transform parent;

    private void Start()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                parent.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
