using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }
    
    void OnAttack()
    {
        if (player.State.CurrentState == player.State.States[Define.StateName.Attack])
            return;
        
        player.State.ChangeState(Define.StateName.Attack);
    }

    void OnSubAttack()
    {
        if (player.State.CurrentState == player.State.States[Define.StateName.Attack])
            return;
        
        // 공격 타입 변경
        (player.State.States[Define.StateName.Attack] as AttackState)?.SetAttackType(AttackType.Sub);
        player.State.ChangeState(Define.StateName.Attack);
    }
    
    public void OnMove()
    {
        if (player.State.CurrentState == player.State.States[Define.StateName.Attack])
            return;
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Ground"));

        if (raycastHit)
        {
            player.Agent.SetDestination(hit.point);
            player.State.ChangeState(Define.StateName.Move);
        }
    }

    public void OnChangeWeapon1() // 검
    {
        if (player.State.CurrentState == player.State.States[Define.StateName.Attack])
            return;
        
        player.Weapon.ChangeWeapon(Define.WeaponName.Sword);
    }
    
    public void OnChangeWeapon2() // 폴암
    {
        if (player.State.CurrentState == player.State.States[Define.StateName.Attack])
            return;
        
        player.Weapon.ChangeWeapon(Define.WeaponName.Polearm);
    }
    
    public void OnChangeWeapon3() // 양손검
    {
        if (player.State.CurrentState == player.State.States[Define.StateName.Attack])
            return;
        
        player.Weapon.ChangeWeapon(Define.WeaponName.TwoHander);
    }

}
