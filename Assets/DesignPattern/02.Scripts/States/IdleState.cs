using UnityEngine;

public class IdleState : IState
{
    Player player = Player.Instance;
    
    public void EnterState()
    {
        // 정지
        player.Character.Move(Vector3.zero);
        
        // 애니메이션
        player.Anim.CrossFade("IDLE", 0.1f);
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }
}
