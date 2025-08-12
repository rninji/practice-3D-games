using System.Collections;
using UnityEngine;

public class AttackState : IState
{
    Player player = Player.Instance;
    
    public void EnterState()
    {
        // 정지
        player.Character.Move(Vector3.zero);
        
        // 코루틴 실행 - 애니메이션
        Player.Instance.Weapon?.Attack();
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }
}
