using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour,IInteractable
{
    private GameManager manager;
    private BoxCollider boxCollider;
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
            counterText.enabled = false;
            manager.speed = 1;
            if (transform.GetChild(1).name == "BrickWall")
            {
                Explosion();
            }
            else if (transform.GetChild(1).name == "Stickman_heads_sphere")
            {
                StickExpo();
            }
        }
    }

    private void Explosion()
    {
        StartCoroutine(ExplosionTime());
    }
    private IEnumerator ExplosionTime()
    {
        boxCollider.enabled = false;
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            transform.GetChild(1).GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
        }
        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().enabled = false;
        }
        GameEvent.Expo();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void StickExpo()
    {
        StartCoroutine(StickExpoTime());
    }

    private IEnumerator StickExpoTime()
    {            
        boxCollider.enabled = false;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().enabled = false;
        }
        GameEvent.Expo();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
