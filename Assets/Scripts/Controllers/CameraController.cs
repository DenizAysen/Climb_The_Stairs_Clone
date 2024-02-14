using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private void OnEnable()
    {
        PlayerController.OnReachedFinalStair += OnReachedFinalStair;
    }

    private void OnReachedFinalStair()
    {
        virtualCamera.m_Follow = null;
    }
    private void OnDisable()
    {
        PlayerController.OnReachedFinalStair -= OnReachedFinalStair;
    }
}
