using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameManager manager;

    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject FailPanel;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject InGamePanel;

    private void Start()
    {
        manager = GameManager.Instance;   
        
        GameEvent.Win += OpenWinPanel;
        GameEvent.Fail += OpenFailPanel;
    }

    public void TapToStartButton()
    {
        StartPanel.SetActive(false);
        InGamePanel.SetActive(true);
        manager.SetGameStage(GameStage.Started);
    }
    private void OpenWinPanel()
    {
        WinPanel.SetActive(true);
        InGamePanel.SetActive(false);
        manager.SetGameStage(GameStage.Win);
    }
    private void OpenFailPanel()
    {
        FailPanel.SetActive(true);
        InGamePanel.SetActive(false);
        manager.SetGameStage(GameStage.Fail);
    }
}
