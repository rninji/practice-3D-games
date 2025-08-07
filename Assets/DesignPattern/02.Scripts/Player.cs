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
    public Animator Anim { get; private set; }
    public CharacterController Character { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            State = new StateMachine();
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
    }

   public void OnMouseEvent()
    {
        if (State.CurrentState == State.states[StateMachine.StateName.Attack])
            return;
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Ground"));

        if (raycastHit)
        {
            Agent.SetDestination(hit.point);
            State.ChangeState(StateMachine.StateName.Move);
        }
    }

    public void OnKeyboardEvent(KeyCode key)
    {
        // 공격
        if (key == KeyCode.LeftControl)
        {
            if (State.CurrentState == State.states[StateMachine.StateName.Attack])
                return;
            State.ChangeState(StateMachine.StateName.Attack);
        }
    }
    
    
}
