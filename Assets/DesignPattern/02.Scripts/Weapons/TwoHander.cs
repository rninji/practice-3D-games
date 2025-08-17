using System.Collections;
using UnityEngine;

public class TwoHander:  Weapon
{
    Player player = Player.Instance;
    private float coolTime = 1.2f;
    
    protected override IEnumerator AttackRoutine()
    {
        player.Anim.CrossFade("TwoHanderAttack", 0.1f); 
        UIManager.Instance.StartCoolTime(coolTime);
        
        yield return new WaitForSeconds(0.1f);// 딜레이
        
        float animDuration = player.Anim.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(animDuration - 0.1f); // 애니메이션 시간만큼 대기
    }
}
