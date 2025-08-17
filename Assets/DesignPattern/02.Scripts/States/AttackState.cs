using System.Collections;
using UnityEngine;

public enum AttackType
{
    Normal,
    Sub
}

public class AttackState : IState
{
    Player player = Player.Instance;
    
    private AttackType attackType = AttackType.Normal;
    
    public void EnterState()
    {
        switch (attackType)
        {
            case AttackType.Normal:
                Attack();
                break;
            case AttackType.Sub:
                SubAttack();
                break;
        }
        
        player.State.ChangeState(Define.StateName.Idle); // Idle로 전환
    }

    public void SetAttackType(AttackType attackType)
    {
        this.attackType = attackType;
    }

    public void Attack()
    {
        // 정지
        player.Character.Move(Vector3.zero);
        
        // 코루틴 실행 - 애니메이션
        Player.Instance.Weapon?.Attack();
    }

    public void SubAttack()
    {
        // 정지
        player.Character.Move(Vector3.zero);
        
        // 코루틴 실행 - 애니메이션
        Player.Instance.Weapon?.SubAttack();

        SetAttackType(AttackType.Normal);
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }
}
