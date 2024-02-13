using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ShopPanelController.onIncreaseSpeed += OnIncreaseSpeed;
    }

    private void OnIncreaseSpeed()
    {
        _animator.SetTrigger("isRunning");
    }
    private void OnDisable()
    {
        ShopPanelController.onIncreaseSpeed -= OnIncreaseSpeed;
    }
    public void PlayClimbingStairsAnim() => _animator.SetBool("ClimbingStairs", true);
    public void StopClimbingStairsAnim() => _animator.SetBool("ClimbingStairs", false);
}
