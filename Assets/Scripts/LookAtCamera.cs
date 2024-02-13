using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool _invert;

    private Transform _cameraTransform;
    private Vector3 _dirToCamera;
    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }
    private void LateUpdate()
    {
        if (!gameObject.activeSelf)
            return;

        if (_invert)
        {
            _dirToCamera = (_cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + _dirToCamera * -1);
        }
        else
        {
            transform.LookAt(_cameraTransform);
        }
    }
}
