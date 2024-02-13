using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static Action OnMoneyIncreased;
    public Action<float> OnMoneySpend;

    private float _money;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _money = GetMoneyValue();
    }
    private void OnEnable()
    {
        OnMoneyIncreased += IncreaseMoney;
        OnMoneySpend += SpendMoney;
    }
    private void OnDisable()
    {
        OnMoneyIncreased -= IncreaseMoney;
        OnMoneySpend -= SpendMoney;
    }
    private float GetMoneyValue()
    {
        return PlayerPrefs.GetFloat("Score", 0f);
    }
    public float GetMoney() => _money;
    public void IncreaseMoney()
    {
        _money += FeatureManager.Instance.GetIncome();
        PlayerPrefs.SetFloat("Score", _money);
    }
    public void SpendMoney(float amount)
    {
        _money -= amount;
        PlayerPrefs.SetFloat("Score", _money);
    }
}
