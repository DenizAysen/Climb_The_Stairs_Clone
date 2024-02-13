using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanelController : MonoBehaviour
{

    [SerializeField] private Button continueButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private TextMeshProUGUI youDidGoodText;

    private int _reviveCount;
    private void Start()
    {
        _reviveCount = 1;
        tryAgainButton.onClick.AddListener(() =>
        {
            LevelManager.OnTryAgain?.Invoke();
        });
        continueButton.onClick.AddListener(() =>
        {
            LevelManager.OnContinue?.Invoke();
            _reviveCount--;
        });
    }
    private void OnEnable()
    {
        SubscribeEvents();       
    }

    private void SubscribeEvents()
    {
        LevelManager.OnTryAgain += OnRestartScene;
        PlayerHealth.OnDeath += OnDeath;
    }

    private void OnRestartScene()
    {
        CloseUIElements();
        _reviveCount = 1;
    }
    private void OnDeath()
    {
        if(_reviveCount <= 0)
            continueButton.gameObject.SetActive(false);
    }

    private void CloseUIElements()
    {
        continueButton.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);
        youDidGoodText.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        LevelManager.OnTryAgain -= OnRestartScene;
    }
}
