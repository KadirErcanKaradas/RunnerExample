using System;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour,IInteractable
{
    [SerializeField] private Vector3 rotateAmount;
    [SerializeField] private float speed;
    
    private void Update()
    {
        Jump();
    }

    public void Interact()
    {
        GameEvent.Collect();
        CollectSphere();
    }

    private void CollectSphere()
    {
        gameObject.SetActive(false);
    }

    private void Jump()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.Rotate(rotateAmount * Time.deltaTime );
            float y = Mathf.PingPong(Time.time * speed, 1);
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }
    }
}
