using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class Sword: Weapon, ISubAttackable
{
    Player player = Player.Instance;

    private float coolTime = 1.2f;
    private float subCoolTime = 0.5f;
    protected override void Init()
    {
        base.Init();
        CreateSubWeapon("Shield");
    }

    public override void Attack()
    {
        player.StartCoroutine(AttackRoutine());
    }
    
    IEnumerator AttackRoutine()
    {
        player.Anim.CrossFade("SwordAttack", 0.1f); 
        UIManager.Instance.StartCoolTime(coolTime);
        
        yield return new WaitForSeconds(0.1f);// 딜레이
        
        float animDuration = player.Anim.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(animDuration - 0.1f); // 애니메이션 시간만큼 대기
    }

    public void SubAttack()
    {
        player.StartCoroutine(SubAttackRoutine());
    }
    
    IEnumerator SubAttackRoutine()
    {
        player.Anim.CrossFade("ShieldAttack", 0.1f); 
        UIManager.Instance.StartCoolTime(subCoolTime, true);
        
        yield return new WaitForSeconds(0.1f);// 딜레이
        
        float animDuration = player.Anim.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(animDuration - 0.1f); // 애니메이션 시간만큼 대기
    }
}
