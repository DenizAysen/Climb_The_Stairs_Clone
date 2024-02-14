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
    [SerializeField] private Image FillBar;
    private void Awake()
    {
        FillBar.fillAmount = .1f;
    }
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
        PlayerController.OnClimb += OnClimb;
    }
    private void OnMoneyIncreased() => moneyText.text = ((int)ScoreManager.Instance.GetMoney()).ToString();
    private void OnClimb(float climbRate)
    {
        FillBar.fillAmount = 0.1f + (climbRate * .9f);
    }
    private void OnDisable()
    {
        ScoreManager.OnMoneyIncreased -= OnMoneyIncreased;
        PlayerController.OnClimb -= OnClimb;
    }
}
