using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour,IState
{
    PlayerController playerController;
    public void Enter()
    {
        //UIManager.Current.OpenGamePanel();
        playerController.enabled = true;
    }

    public void Exit()
    {
        //UIManager.Current.CloseGamePanel();
        playerController.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
}
