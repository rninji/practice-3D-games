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

    public Transform rightGripPoint;
    public Transform leftGripPoint;

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
    }
   
}
