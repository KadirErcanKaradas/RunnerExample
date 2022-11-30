using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour,IEnemy
{
    private GameManager manager;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private int counterNumber;
    private void Awake()
    {
        counterText.text = counterNumber.ToString();
    }

    private void Start()
    {
        manager = GameManager.Instance;
    }

    public void DestroyCube()
    {
        GameEvent.NotCollect();
        LoseSphere();
    }
    
    public void LoseSphere()
    {
        float scale = transform.GetChild(0).localScale.magnitude;
        if (counterNumber>1)
        {
            counterNumber--;
            Vector3 enemyVec = transform.GetChild(0).localScale;
            transform.GetChild(0).DOScale(new Vector3(enemyVec.x, enemyVec.y, enemyVec.z - scale/10), 0.1f);
            counterText.text = counterNumber.ToString();
        }
        else if(counterNumber ==1)
        {
            manager.speed = 5;
            gameObject.SetActive(false);
        }
    }
    
}
