using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    public static Action onPausePanelOpen;

    [SerializeField] private Button pausebutton;
    [SerializeField] private TextMeshProUGUI moneyText;
    private void Start()
    {
        pausebutton.onClick.AddListener(() => {
            onPausePanelOpen?.Invoke();
        });
        moneyText.text = ((int)ScoreManager.Instance.GetMoney()).ToString();
    }
    private void OnEnable()
    {
        ScoreManager.OnMoneyIncreased += OnMoneyIncreased;
    }

    private void OnMoneyIncreased() => moneyText.text = ((int)ScoreManager.Instance.GetMoney()).ToString();
    private void OnDisable()
    {
        ScoreManager.OnMoneyIncreased -= OnMoneyIncreased;
    }
}
