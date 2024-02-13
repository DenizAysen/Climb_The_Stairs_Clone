using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelController : MonoBehaviour
{
    public static Action onPlay;
    public static Action onIncreaseStamina;
    public static Action onIncreaseIncome;
    public static Action onIncreaseSpeed;

    #region SerializedFields
    [SerializeField] private Button tapToPlayButton;
    [SerializeField] private Button staminaLevelUpgradeButtton;
    [SerializeField] private Button incomeLevelUpgradeButtton;
    [SerializeField] private Button speedLevelUpgradeButtton;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI staminaLevelText;
    [SerializeField] private TextMeshProUGUI incomeLevelText;
    [SerializeField] private TextMeshProUGUI speedLevelText;
    #endregion
    private int _money;
    private int _staminaLevel;
    private int _incomeLevel;
    private int _speedLevel;
    private void OnEnable()
    {
        onIncreaseStamina += OnIncreaseStamina;
        onIncreaseIncome += OnIncreaseIncome;
        onIncreaseSpeed += OnIncreaseSpeed;
    }

    private void OnIncreaseStamina()
    {
        _staminaLevel = FeatureManager.Instance.GetStaminaLevel();
        staminaLevelText.SetText("lvl " + _staminaLevel);
        Refresh();
    }

    private void OnIncreaseIncome()
    {
        _incomeLevel = FeatureManager.Instance.GetIncomeLevel();
        incomeLevelText.SetText("lvl " + _incomeLevel);
        Refresh();
    }
    private void OnIncreaseSpeed()
    {
        _speedLevel = FeatureManager.Instance.GetSpeedLevel();
        speedLevelText.SetText("lvl " + _speedLevel);
        Refresh();
    }
    private void OnDisable()
    {
        onIncreaseStamina -= OnIncreaseStamina;
        onIncreaseIncome -= OnIncreaseIncome;
        onIncreaseSpeed -= OnIncreaseSpeed;
    }
    private void Start()
    {
        _money = (int)ScoreManager.Instance.GetMoney();
        _staminaLevel = FeatureManager.Instance.GetStaminaLevel();
        _incomeLevel = FeatureManager.Instance.GetIncomeLevel();
        _speedLevel = FeatureManager.Instance.GetSpeedLevel();
        SyncShopUI();
    }
    private void SyncShopUI()
    {
        SetMoneyText();

        SetLevelTexts();

        SetUpgradeCostTexts();

        ChangeInteractables();

        AddListenerToButtons();
    }
    private void SetMoneyText() => moneyText.text = _money.ToString();
    private void SetLevelTexts()
    {
        staminaLevelText.SetText("lvl " + _staminaLevel);
        incomeLevelText.SetText("lvl " + _incomeLevel);
        speedLevelText.SetText("lvl " + _speedLevel);
    }
    private void SetUpgradeCostTexts()
    {
        staminaLevelUpgradeButtton.GetComponentInChildren<TextMeshProUGUI>().text = FeatureManager.Instance.
            GetUpgradeCostAmount(_staminaLevel).ToString();
        incomeLevelUpgradeButtton.GetComponentInChildren<TextMeshProUGUI>().text = FeatureManager.Instance.
           GetUpgradeCostAmount(_incomeLevel).ToString();
        speedLevelUpgradeButtton.GetComponentInChildren<TextMeshProUGUI>().text = FeatureManager.Instance.
           GetUpgradeCostAmount(_speedLevel).ToString();
    }
    private void ChangeInteractables()
    {
        ChangesStaminaInteractable();

        ChangesIncomeInteractable();

        ChangesSpeedInteractable();
    }
    private void ChangesStaminaInteractable()
    {
        if (_money < FeatureManager.Instance.GetUpgradeCostAmount(_staminaLevel) || _staminaLevel == 5)
        {
            staminaLevelUpgradeButtton.interactable = false;
        }
        else
        {
            staminaLevelUpgradeButtton.interactable = true;
        }
    }
    private void ChangesIncomeInteractable()
    {
        if(_money < FeatureManager.Instance.GetUpgradeCostAmount(_incomeLevel) || _incomeLevel == 5)
        {
            incomeLevelUpgradeButtton.interactable = false;
        }
        else
        {
            incomeLevelUpgradeButtton.interactable = true;
        }
    }
    private void ChangesSpeedInteractable()
    {
        if (_money < FeatureManager.Instance.GetUpgradeCostAmount(_speedLevel) || _speedLevel == 5)
        {
            speedLevelUpgradeButtton.interactable = false;
        }
        else
        {
            speedLevelUpgradeButtton.interactable = true;
        }
    }
    private void Refresh()
    {
        _money = (int)ScoreManager.Instance.GetMoney();
        SetMoneyText();
        SetUpgradeCostTexts();
        ChangeInteractables();
    }
    private void AddListenerToButtons()
    {
        tapToPlayButton.onClick.AddListener(() =>
        {
            onPlay?.Invoke();
        });
        staminaLevelUpgradeButtton.onClick.AddListener(() =>
        {
            ScoreManager.Instance.SpendMoney(FeatureManager.Instance.
                GetUpgradeCostAmount(_staminaLevel));
            onIncreaseStamina?.Invoke();
        });
        incomeLevelUpgradeButtton.onClick.AddListener(() =>
        {
            ScoreManager.Instance.SpendMoney(FeatureManager.Instance.
                GetUpgradeCostAmount(_incomeLevel));
            onIncreaseIncome?.Invoke();           
        });

        speedLevelUpgradeButtton.onClick.AddListener(() =>
        {
            ScoreManager.Instance.SpendMoney(FeatureManager.Instance.
                GetUpgradeCostAmount(_speedLevel));
            onIncreaseSpeed?.Invoke();
        });
    }
}
