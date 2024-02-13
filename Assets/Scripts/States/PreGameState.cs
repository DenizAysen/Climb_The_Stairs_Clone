using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameState : MonoBehaviour,IState
{
    public void Enter()
    {
        UIManager.Instance.OpenPreGamePanel();
    }

    public void Exit()
    {
        UIManager.Instance.ClosePreGamePanel();
    }

    
}
