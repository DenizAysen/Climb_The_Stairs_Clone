using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static Action<float> OnTakeDamage;
    public static Action OnRegenerateHealth;

    public static Action OnDeath;

    #region Serialized Fields
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float healthRegenSpeed = 2f; 
    #endregion

    #region Privates
    private float _currentHealth;
    private PlayerEffectController _effectController;

    private bool _isDead;
    #endregion
    private void Awake()
    {
        _currentHealth = maxHealth;
        _isDead = false;
        _effectController = GetComponent<PlayerEffectController>();
    }
    private void OnEnable()
    {
        SubscriveEvents();
    }

    private void SubscriveEvents()
    {
        LevelManager.OnContinue += OnContinue;
        ShopPanelController.onPlay += OnPlay;
    }
    private void OnContinue()
    {
        _currentHealth = maxHealth / 4;
        _effectController.DestroyAllParticles();
        _isDead = false;
    }
    private void OnPlay()
    {
        maxHealth = FeatureManager.Instance.GetMaxHealth();
        _currentHealth = maxHealth;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        LevelManager.OnContinue -= OnContinue;
        ShopPanelController.onPlay -= OnPlay;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        //Debug.Log(_currentHealth);
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            _effectController.CreateBloodExplosionParticles();
            _isDead = true;
            OnDeath?.Invoke();
        }
        else
        {
            if(GetNormalizedHealthValue() <= .5f)
            {
                int sweatingParticlesAmount = Mathf.RoundToInt((1 - GetNormalizedHealthValue()) * 10);
                _effectController.CreateSweatingParticles(sweatingParticlesAmount);
            }
            if(GetNormalizedHealthValue() <= .25f)
                OnTakeDamage?.Invoke((maxHealth - _currentHealth) / maxHealth);
        }
    }
    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth < maxHealth)


        if (_currentHealth > maxHealth)
            _currentHealth = maxHealth;
        
        Debug.Log("Player healed and current health : " + _currentHealth);
    }
    public void RegenerateHealth()
    {
        if (!(_currentHealth < maxHealth) || _isDead)
            return;

        OnRegenerateHealth?.Invoke();
        _currentHealth += Time.deltaTime * healthRegenSpeed;
        if (_currentHealth > maxHealth)
            _currentHealth = maxHealth;
        if (_currentHealth >= maxHealth / 2)
            _effectController.DestroySweatingParticles();
        //Debug.Log(_currentHealth);
    }
    public bool IsPlayerFullHealth() => _currentHealth == maxHealth;
    private void IncreaseMaxHealth(float maxHealth) => this.maxHealth = maxHealth;
    private float GetNormalizedHealthValue() => _currentHealth / maxHealth;
}
