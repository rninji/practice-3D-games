using System.Collections.Generic;
using UnityEngine;


public class StateMachine
{
    public IState CurrentState { get; private set; }
    public Dictionary<Define.StateName, IState> States { get; private set; }  = new Dictionary<Define.StateName, IState>();

    public StateMachine()
    {
        States.Add(Define.StateName.Idle, new IdleState());
        States.Add(Define.StateName.Move, new MoveState());
        States.Add(Define.StateName.Attack, new AttackState());
    }
    
    // 초기화
    public void InitState()
    {
        CurrentState = States[Define.StateName.Idle];
        CurrentState?.EnterState();
    }

    // 상태 전환
    public void ChangeState(Define.StateName nextState)
    {
        if (States.TryGetValue(nextState, out IState newState) == false)
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
