using System.Collections;
using UnityEngine;

public class TwoHander:  Weapon
{
    Player player = Player.Instance;

    public override void Attack()
    {
        player.StartCoroutine(AttackRoutine());
    }
    
    IEnumerator AttackRoutine()
    {
        player.Anim.CrossFade("TwoHanderAttack", 0.1f); 
        
        yield return new WaitForSeconds(0.1f);// 딜레이
        
        float animDuration = player.Anim.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(animDuration - 0.1f); // 애니메이션 시간만큼 대기
    }
}
