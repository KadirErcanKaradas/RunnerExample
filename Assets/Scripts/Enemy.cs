using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour,IEnemy
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

    public void DestroyCube()
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
            Explosion();
        }
    }

    private void Explosion()
    {
        StartCoroutine(ExplosionTime());
    }
    private IEnumerator ExplosionTime()
    {
        boxCollider.enabled = false;
        GameEvent.Expo();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    
}
