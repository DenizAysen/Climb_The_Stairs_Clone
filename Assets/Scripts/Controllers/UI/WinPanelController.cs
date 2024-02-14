using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelController : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    private void Start()
    {
        nextButton.onClick.AddListener(() =>
        {
            LevelManager.OnNextLevel?.Invoke();
        });
    }
}
