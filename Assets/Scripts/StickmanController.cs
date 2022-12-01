using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class StickmanController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        int randomNumber = Random.Range(1, 4);
        animator.SetBool(randomNumber.ToString(),true);
    }
}
