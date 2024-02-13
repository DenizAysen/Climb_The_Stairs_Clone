using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Current;
    void Awake()
    {
        if(Current == null)
        {
            Current = this;
        }
    }

 
    [SerializeField]
    private List<State> _gameStates = new List<State>();

    private static IState _currentState;
   // private SaveManager _saveManager;

    private void Start()
    {
        SetState(StateType.PreGameState);
    }


    #region STATE MACHINE
    public void SetState(StateType stateType)
    {
        IState nextState = _gameStates.FirstOrDefault(x => x.stateType == stateType).stateScript as IState;

        if (_currentState == nextState) return;
        if (_currentState != null) _currentState.Exit();

        _currentState = nextState;
        nextState.Enter();
    }

    public IState GetCurrentState()
    {
        return _currentState;
    }

    #endregion
    /*
    #region Managers
    public void SetSaveManager(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    public void SetMaxScore(int score)
    {
        _saveManager.SaveMaxScore(score);
    }

    public int GetMaxScore()
    {
        return _saveManager.GetMaxScore();
    }

    #endregion*/
}

[System.Serializable]
public class State
{
    public StateType stateType;
    public MonoBehaviour stateScript;
}

public enum StateType
{
    PreGameState,
    GameState,
    LoseGameState,
    WinGameState
}

