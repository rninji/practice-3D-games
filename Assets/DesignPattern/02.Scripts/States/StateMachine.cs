using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public enum StateName { Idle, Move, Attack }
    public IState CurrentState { get; private set; }
    public Dictionary<StateName, IState> states = new Dictionary<StateName, IState>();

    public StateMachine()
    {
        states.Add(StateName.Idle, new IdleState());
        states.Add(StateName.Move, new MoveState());
        states.Add(StateName.Attack, new AttackState());
    }
    
    // 초기화
    public void InitState()
    {
        CurrentState = states[StateName.Idle];
        CurrentState?.EnterState();
    }

    // 상태 전환
    public void ChangeState(StateName nextState)
    {
        if (states.TryGetValue(nextState, out IState newState) == false)
            return;
        
        if (CurrentState == newState)
            return;
        
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState?.EnterState();
    }
    
    // Update
    public void UpdateState()
    {
        CurrentState?.UpdateState();
    }
}
