using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[AddComponentMenu("")]
public class LockCinemachineAxis : CinemachineExtension
{
    [Tooltip("Lock the Cinemachine Virtual Cameras x axis position")]
    public float XClampValue = 0f;
    public float ZClampValue = -8.78f;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = XClampValue;
            pos.z = ZClampValue;
            state.RawPosition = pos;
        }

    }
}
