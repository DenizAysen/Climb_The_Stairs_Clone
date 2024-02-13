using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        OpenPreGamePanel();
    }
    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        ShopPanelController.onPlay += OnGamePanelOpen;
        GamePanelController.onPausePanelOpen += OnPausePanelOpen;
        LevelManager.OnContinue += OnContinue;
        PausePanelController.OnPausePanelClosed += OnPausePanelClosed;

        PlayerHealth.OnDeath += OnDeath;
    }
    private void OnGamePanelOpen()
    {
        gamePanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    private void OnPausePanelOpen()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnContinue()
    {
        losePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    private void OnPausePanelClosed()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void OnDeath()
    {
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
    }

   
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        ShopPanelController.onPlay -= OnGamePanelOpen;
        GamePanelController.onPausePanelOpen -= OnPausePanelOpen;
        LevelManager.OnContinue -= OnContinue;
        PausePanelController.OnPausePanelClosed -= OnPausePanelClosed;

        PlayerHealth.OnDeath -= OnDeath;
    }

    public void TapToPlayButton()
    {
        ClosePreGamePanel();
        gamePanel.SetActive(true);
    }
    public void OpenPreGamePanel()
    {
        shopPanel.gameObject.SetActive(true);
    }
    public void ClosePreGamePanel()
    {
        shopPanel.gameObject.SetActive(false);
    }
    public void OpenGameOverPanel()
    {
        losePanel.gameObject.SetActive(true);
    }
    public void CloseGameOverPanel()
    {
        losePanel.gameObject.SetActive(false);
    }
    public void TryAgainButton()
    {
        SceneManager.LoadScene(0);
    }
}
