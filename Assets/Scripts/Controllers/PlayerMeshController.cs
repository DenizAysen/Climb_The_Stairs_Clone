using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Color _defaultColor;
    private void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _defaultColor = _skinnedMeshRenderer.material.color;
    }
    private void OnEnable()
    {
        //PlayerController.OnPlayerTired += OnPlayerTired;
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        PlayerHealth.OnTakeDamage += OnTakeDamage;
        PlayerHealth.OnRegenerateHealth += OnRegenerateHealth;
        PlayerHealth.OnDeath += OnDeath;
        LevelManager.OnContinue += OnContinue;
    }
    private void OnTakeDamage(float missingHealthPercantage)
    {
        int meshMatColorChangeSpeed = (int)(missingHealthPercantage * 100);
        _skinnedMeshRenderer.material.color = Color.Lerp(_skinnedMeshRenderer.material.color, Color.red, meshMatColorChangeSpeed * Time.deltaTime);
    }
    public void OnRegenerateHealth()
    {
        _skinnedMeshRenderer.material.color = Color.Lerp(_skinnedMeshRenderer.material.color, _defaultColor, .25f * Time.deltaTime);
    }
    private void OnDeath()
    {
        _skinnedMeshRenderer.enabled = false;
    }
    private void OnContinue()
    {
        _skinnedMeshRenderer.enabled = true;
        _skinnedMeshRenderer.material.color = _defaultColor;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        LevelManager.OnContinue -= OnContinue;
        PlayerHealth.OnTakeDamage -= OnTakeDamage;
        PlayerHealth.OnDeath -= OnDeath;
        PlayerHealth.OnRegenerateHealth -= OnRegenerateHealth;
    }
}
