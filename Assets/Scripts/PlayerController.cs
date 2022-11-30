using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] private float speed = 5;
    
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private int counterNumber;

    private void Awake()
    {
        counterText.text = counterNumber.ToString();
        GameEvent.Collect += ScaleUp;
        GameEvent.NotCollect += ScaleDown;
    }

    private void Start()
    {
        manager = GameManager.Instance;
    }

    private void Update()
    {
        if (manager.GameStage == GameStage.Started)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactable.Interact();
        
        var interactable1 = other.GetComponent<IEnemy>();
        if (interactable1 != null)
           interactable1.TimeSlowDown();
    }
    
    private void OnTriggerStay(Collider other)
    {
        var interactable = other.GetComponent<IEnemy>();
        if(interactable == null) return;
        interactable.DestroyCube();
    }

    private void ScaleUp()
    {
        Vector3 plusVec = new Vector3(0.02f, 0.02f, 0.02f);
        Vector3 playerVec = transform.localScale;
        transform.DOScale(new Vector3(playerVec.x + plusVec.x, playerVec.y + plusVec.y, playerVec.z + plusVec.z), 0.1f);
        transform.position += new Vector3(0, 0.01f, 0); 
        counterNumber++;
        counterText.text = counterNumber.ToString();
    }

    private void ScaleDown()
    {
        Vector3 negoVec = new Vector3(0.02f, 0.02f, 0.02f);
        Vector3 playerVec = transform.localScale;
        transform.DOScale(new Vector3(playerVec.x - negoVec.x, playerVec.y - negoVec.y, playerVec.z - negoVec.z), 0.1f);
        transform.position -= new Vector3(0, 0.01f, 0); 
        if (counterNumber>0)
        {
            counterNumber--;
            counterText.text = counterNumber.ToString();
        }
        else if (counterNumber == 0)
        {
            manager.isTimeNormal = true;
            GameEvent.Fail();
            gameObject.SetActive(false);
            speed = 0;
        }
    }
}
