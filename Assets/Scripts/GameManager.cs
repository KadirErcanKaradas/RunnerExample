using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public GameStage GameStage { get; private set; }
    public float speed = 5;

    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;
    }
    
}
public enum GameStage
{
    NotLoaded, Loaded, Started, Win, Fail, Cannon
}