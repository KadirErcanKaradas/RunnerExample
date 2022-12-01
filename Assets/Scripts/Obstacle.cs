using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour,IInteractable
{
    private GameManager manager;
    public BoxCollider boxCollider;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private int counterNumber;
    private void Awake()
    {
        counterText.text = counterNumber.ToString();
    }
    private void Start()
    {
        manager = GameManager.Instance;
        boxCollider = GetComponent<BoxCollider>();
    }
    public void Interact()
    {
        GameEvent.Obstacle();
        LoseSphere();
    }
    public void LoseSphere()
    {
        if (counterNumber>1)
        {
            counterNumber--;
            counterText.text = counterNumber.ToString();
        }
        else if(counterNumber ==1)
        {
            boxCollider.enabled = false;
            counterText.enabled = false;
            Explosion();
        }
    }

    protected virtual void Explosion()
    {
        Debug.Log("GİRDİ PLAYER");
    }

}
