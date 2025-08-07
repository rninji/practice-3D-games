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
        player.StartCoroutine(AttackRoutine());
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }

    IEnumerator AttackRoutine()
    {
        player.Anim.CrossFade("ATTACK", 0.1f); 
        
        yield return new WaitForSeconds(0.1f);// 딜레이
        
        float animDuration = player.Anim.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(animDuration - 0.1f); // 애니메이션 시간만큼 대기
        
        player.State.ChangeState(StateMachine.StateName.Idle); // Idle로 전환
    }
    
}
