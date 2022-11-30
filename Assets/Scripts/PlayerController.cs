using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private GameManager manager;
    private ObjectPool pool;
    [SerializeField] private float speed = 5;
    
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private int counterNumber;

    private void Awake()
    {
        counterText.text = counterNumber.ToString();
    }

    private void OnEnable()
    {
        GameEvent.Collect += ScaleUp;
        GameEvent.NotCollect += ScaleDown;
    }

    private void OnDisable()
    {
        GameEvent.Collect -= ScaleUp;
        GameEvent.NotCollect -= ScaleDown;
    }

    private void Start()
    {
        manager = GameManager.Instance;
        pool = ObjectPool.Instance;
    }

    private void Update()
    {
        if (manager.GameStage == GameStage.Started)
            transform.Translate(Vector3.forward * manager.speed * Time.deltaTime);
        
        print(manager.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactable.Interact();
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
        GameObject obj = pool.GetPooledObject(0);
        obj.SetActive(true);
        obj.transform.localPosition = transform.position;
        transform.DOScale(new Vector3(playerVec.x - negoVec.x, playerVec.y - negoVec.y, playerVec.z - negoVec.z), 0.1f);
        transform.position -= new Vector3(0, 0.01f, 0); 
        manager.speed = 1f;
        if (counterNumber>0)
        {
            counterNumber--;
            counterText.text = counterNumber.ToString();
        }
        else if (counterNumber == 0)
        {
            GameEvent.Fail();
            gameObject.SetActive(false);
            manager.speed = 0;
        }
    }
}
