using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelController : MonoBehaviour
{
    public static Action OnPausePanelClosed;
    public static Action OnRestartScene;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    private void Start()
    {
        resumeButton.onClick.AddListener(() =>
        {
            OnPausePanelClosed?.Invoke();
        });
        restartButton.onClick.AddListener(() =>
        {
            OnRestartScene?.Invoke();
        });
    }
}
