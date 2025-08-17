using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Weapon : MonoBehaviour
{
    List<GameObject> objects;


    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        objects = new List<GameObject>();
        objects.Add(gameObject);
    }
    
    protected void CreateSubWeapon(string name, bool isLeft = true)
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{name}");
        Transform gripPoint = isLeft ? Player.Instance.leftGripPoint : Player.Instance.rightGripPoint;
        GameObject obj = Object.Instantiate(prefab, gripPoint);
        obj.SetActive(false);
        objects.Add(obj);
        
        UIManager.Instance.AddSubAttackIcon(Define.WeaponName.Sword, "Shield");
    }

    public void ActiveWeapons(bool isActive = true)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(isActive);
        }
    }
    
    public virtual IEnumerator Attack()
    {
        yield return Player.Instance.StartCoroutine(AttackRoutine());
        Player.Instance.State.ChangeState(Define.StateName.Idle); // Idle로 전환
    }
    
    protected abstract IEnumerator AttackRoutine();
}
