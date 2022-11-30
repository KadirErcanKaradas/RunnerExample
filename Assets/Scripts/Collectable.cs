using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        GameEvent.Collect();
        CollectSphere();
    }

    private void CollectSphere()
    {
        gameObject.SetActive(false);
    }
}
