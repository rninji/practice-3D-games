using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 플레이어의 정보를 담고있는 클래스
/// Singleton 패턴 사용
/// </summary>
public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player Instance { get { return instance; } }
    public StateMachine State { get; private set; }
    public WeaponManager Weapon { get; private set; }
    public Animator Anim { get; private set; }
    public CharacterController Character { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    public Transform gripPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            State = new StateMachine();
            Weapon = new WeaponManager();
            Anim = gameObject.GetComponent<Animator>();
            Character = gameObject.GetComponent<CharacterController>();
            Agent = gameObject.GetComponent<NavMeshAgent>();
            Agent.updatePosition = false;
            Agent.updateRotation = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        Weapon?.InitWeapon();
        State?.InitState();
    }

    void Update()
    {
        State?.UpdateState();
        
        // Mouse Input
        if (Input.GetMouseButton(0))
            OnMouseEvent();
        
        // Keyboard Input
        if (Input.GetKeyDown(KeyCode.LeftControl))
            OnKeyboardEvent(KeyCode.LeftControl);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnKeyboardEvent(KeyCode.Alpha1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            OnKeyboardEvent(KeyCode.Alpha2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            OnKeyboardEvent(KeyCode.Alpha3);
    }

   public void OnMouseEvent()
    {
        if (State.CurrentState == State.States[Define.StateName.Attack])
            return;
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Ground"));

        if (raycastHit)
        {
            Agent.SetDestination(hit.point);
            State.ChangeState(Define.StateName.Move);
        }
    }

    public void OnKeyboardEvent(KeyCode key)
    {
        // 공격
        if (key == KeyCode.LeftControl)
        {
            if (State.CurrentState == State.States[Define.StateName.Attack])
                return;
            State.ChangeState(Define.StateName.Attack);
        }
        
        if (State.CurrentState == State.States[Define.StateName.Attack])
            return;
        
        // 무기 교체
        if (key == KeyCode.Alpha1)
            Weapon.ChangeWeapon(Define.WeaponName.Sword);
        else if (key == KeyCode.Alpha2)
            Weapon.ChangeWeapon(Define.WeaponName.Polearm);
        else if (key == KeyCode.Alpha3)
            Weapon.ChangeWeapon(Define.WeaponName.TwoHander);
    }
}
