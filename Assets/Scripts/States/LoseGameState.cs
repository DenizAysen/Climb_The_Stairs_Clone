using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGameState : MonoBehaviour,IState
{
    public void Enter()
    {
        //UIManager.Instance.OpenGameOverPanel();
    }

    public void Exit()
    {
        //UIManager.Instance.CloseGameOverPanel();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
