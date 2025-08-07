using UnityEngine;
using UnityEngine.AI;

public class MoveState : IState
{
    Player player = Player.Instance;
    
    public void EnterState()
    {
        player.Anim.CrossFade("MOVE", 0.1f);
    }

    public void UpdateState()
    {
        // 이동
        player.Character.Move(player.Agent.velocity * Time.deltaTime);
        
        if (player.Agent.remainingDistance < player.Agent.stoppingDistance)
        {
            player.State.ChangeState(StateMachine.StateName.Idle); // 정지
        }
    }

    public void ExitState()
    {
        
    }
}
