using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static Action OnContinue;
    public static Action OnTryAgain;

    [SerializeField] private GameObject player;
    private void OnEnable()
    {
        SubscribeEvents();       
    }

    private void SubscribeEvents()
    {
        OnContinue += ActivatePlayer;
        OnTryAgain += RestartScene;
        PausePanelController.OnRestartScene += OnRestartScene;
    }
    private void ActivatePlayer()
    {
        player.SetActive(true);
    }
    private void RestartScene()
    {
        StartCoroutine(RestartSceneAfterDelay());
    }
    private IEnumerator RestartSceneAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }  
    private void OnRestartScene()
    {
        SceneManager.LoadScene(0);
    }
    private void OnDisable()
    {
        UnSubscribeEvents();      
    }

    private void UnSubscribeEvents()
    {
        OnContinue -= ActivatePlayer;
        OnTryAgain -= RestartScene;
        PausePanelController.OnRestartScene -= OnRestartScene;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
}
