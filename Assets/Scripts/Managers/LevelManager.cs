using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Actions
    public static Action OnContinue;
    public static Action OnTryAgain;
    public static Action OnNextLevel;
    #endregion

    public static LevelManager Instance;

    [SerializeField] private GameObject player;

    private int _levelIndex;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _levelIndex = PlayerPrefs.GetInt("Level", 0);
    }
    public int Level { get { return _levelIndex + 1; } }
    private void OnEnable()
    {
        SubscribeEvents();       
    }

    private void SubscribeEvents()
    {
        OnContinue += ActivatePlayer;
        OnTryAgain += RestartScene;
        OnNextLevel += LoadNextLevel;
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
    private void LoadNextLevel()
    {
        _levelIndex++;
        PlayerPrefs.SetInt("Level", _levelIndex);
        StartCoroutine(LoadNextlevelAfterDelay());
    }
    private IEnumerator LoadNextlevelAfterDelay()
    {
        yield return new WaitForSeconds(1f);
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
        OnNextLevel -= LoadNextLevel;
        PausePanelController.OnRestartScene -= OnRestartScene;
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
}
