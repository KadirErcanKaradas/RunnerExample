using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputSystem inputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    [SerializeField] private float swerveArea;
    [SerializeField] private float clampArea = 4;
    [SerializeField] private GameObject player;
    private GameManager manager;
    

    private void Start()
    {
        if (player == null)
        {
            player = gameObject;
        }
        inputSystem = InputSystem.Instance;
        manager = GameManager.Instance;
    }

    private void Update()
    {
        if (manager.GameStage == GameStage.Started)
        {
            float swerveAmount = Time.deltaTime * swerveSpeed * inputSystem.MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);


            if (player.transform.position.x <= 6 && player.transform.position.x >= 6 * -1)
            {
                player.transform.Translate(swerveAmount, 0, 0);
            }
            else
            {
                if (player.transform.position.x >= swerveArea)
                {
                    player.transform.position = new Vector3(swerveArea, player.transform.position.y, player.transform.position.z);

                }
                else if (player.transform.position.x <= swerveArea * -1f)
                {
                    player.transform.position = new Vector3(swerveArea * -1, player.transform.position.y, player.transform.position.z);
                }
            }
            float clamp = Mathf.Clamp(player.transform.position.x, -clampArea, clampArea);
            player.transform.position = new Vector3(clamp, player.transform.position.y, player.transform.position.z);
        }

    }
}
