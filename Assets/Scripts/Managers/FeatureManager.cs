using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    #region Privates
    private List<int> _costList;

    private int _staminaLevel = 1;
    private int _incomeLevel = 1;
    private int _speedLevel = 1;
    #endregion

    #region Singleton
    public static FeatureManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _costList = GetCostList();

        _staminaLevel = LoadStaminaData();
        _incomeLevel = LoadIncomeData();
        _speedLevel = LoadSpeedData();
    }
    #endregion
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        ShopPanelController.onIncreaseStamina += OnIncreaseStamina;
        ShopPanelController.onIncreaseIncome += OnIncreseIncome;
        ShopPanelController.onIncreaseSpeed += OnIncreseSpeed;
    }

    private void OnIncreaseStamina()
    {
       _staminaLevel++;
       _staminaLevel = Mathf.Clamp(_staminaLevel, 1, 5);
       PlayerPrefs.SetInt("Stamina", _staminaLevel);
    }

    private void OnIncreseIncome()
    {
        _incomeLevel++;
        _incomeLevel = Mathf.Clamp(_incomeLevel, 1, 5);
        PlayerPrefs.SetInt("Income", _incomeLevel);
        Debug.Log(_incomeLevel);
    }
    private void OnIncreseSpeed()
    {
        _speedLevel++;
        _speedLevel =  Mathf.Clamp(_speedLevel, 1, 5);
        PlayerPrefs.SetInt("Speed", _speedLevel);
    }
    private void OnDisable()
    {
        UnSubscribeEvents();       
    }

    private void UnSubscribeEvents()
    {
        ShopPanelController.onIncreaseStamina -= OnIncreaseStamina;
        ShopPanelController.onIncreaseIncome -= OnIncreseIncome;
        ShopPanelController.onIncreaseSpeed -= OnIncreseSpeed;
    }

    private List<int> GetCostList()
    {
        return Resources.Load<UpgradeCostList>("CostList").nextlevelCostList;
    }
    private int LoadStaminaData() => PlayerPrefs.GetInt("Stamina", 1);
    private int LoadIncomeData() => PlayerPrefs.GetInt("Income", 1);
    private int LoadSpeedData() => PlayerPrefs.GetInt("Speed", 1);
    public int GetStaminaLevel() => _staminaLevel;
    public int GetIncomeLevel() => _incomeLevel;
    public int GetSpeedLevel() => _speedLevel;
    public int GetUpgradeCostAmount(int level) => _costList[level];
    public float GetIncome() => 0.5f * _incomeLevel;
    public float GetClimbSpeed() => 0.5f - (_speedLevel * .06f);
    public float GetMaxHealth()
    {
        if (_staminaLevel == 1)
            return 100f;
        return 100f + (150 * _staminaLevel);
    }
}
