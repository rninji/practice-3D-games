using System;
using UnityEngine;

public class IdleState : IState
{
    Player player = Player.Instance;
    
    public void EnterState()
    {
        // 정지
        player.Character.Move(Vector3.zero);
        
        // 애니메이션
        WeaponAnimation(player.Weapon.CurrentWeapon);
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public void WeaponAnimation(Weapon weapon)
    {
        string wpName = weapon.GetType().Name;
        player.Anim.CrossFade($"{wpName}Idle", 0.1f);
    }
}
