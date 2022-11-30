using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float slowDownFactor = 0.01f;
    [SerializeField] private float slowDownLenght = 2f;
    public bool isTimeNormal = true;

    public GameStage GameStage { get; private set; }

    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;
    }
    private void Update()
    {
        if (isTimeNormal)
        {
            Time.timeScale = 1;
            //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    public void DoSlowMotion()
    {
        isTimeNormal = false;
        Time.timeScale = slowDownFactor;
    }
}
public enum GameStage
{
    NotLoaded, Loaded, Started, Win,WinCube, Fail,FailFall
}