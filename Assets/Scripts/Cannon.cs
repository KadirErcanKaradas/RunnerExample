using UnityEngine;

public class Cannon : MonoBehaviour,IInteractable
{
    private GameManager manager;
    private BoxCollider boxCollider;

    private void Start()
    {
        manager = GameManager.Instance;
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Interact()
    {
        boxCollider.enabled = false;
        manager.speed = 0f;
        GameEvent.CannonFire();
    }
    
}
