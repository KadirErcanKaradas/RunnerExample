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
    private Rigidbody rb;
    private Vector3 rotateAmount = new Vector3(0,360,0);

    public Transform cannonPos;

    [SerializeField] private TMP_Text counterText;
    [SerializeField] private int counterNumber;
    [SerializeField] private ParticleSystem explosionParticle;

    private void Awake()
    {
        counterText.text = counterNumber.ToString();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        GameEvent.Collect += ScaleUp;
        GameEvent.Obstacle += ScaleDown;
        GameEvent.Expo += ExplosionParticle;
        GameEvent.CannonFire += FireCannon;
    }

    private void OnDisable()
    {
        GameEvent.Collect -= ScaleUp;
        GameEvent.Obstacle -= ScaleDown;
        GameEvent.Expo -= ExplosionParticle;
        GameEvent.CannonFire -= FireCannon;
    }

    private void Start()
    {
        manager = GameManager.Instance;
        pool = ObjectPool.Instance;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (manager.GameStage == GameStage.Started)
        {
            transform.Translate(Vector3.forward * manager.speed * Time.deltaTime);
            transform.GetChild(0).Rotate(rotateAmount * Time.deltaTime);
        }
        if (manager.GameStage == GameStage.Cannon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.isKinematic = false;
                rb.velocity = counterNumber * 2 *new Vector3(0,0.4f,1);
                counterText.gameObject.SetActive(false);
                manager.SetGameStage(GameStage.Win);
            }
        }
        if (manager.GameStage == GameStage.Win)
        {
            if (rb.velocity.z <= 4.2f)
            {
                GameEvent.Win();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactable.Interact();
    }
    
    private void OnTriggerStay(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactable.Interact();
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
    private void ExplosionParticle()
    {
        explosionParticle.gameObject.SetActive(true);
        explosionParticle.Play();
        manager.speed = 5;
    }
    
    private void FireCannon()
    {
        manager.SetGameStage(GameStage.Cannon);
        transform.DOLocalJump(cannonPos.position, 3, 1, 1);
    }
}
