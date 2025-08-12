using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;



public class WeaponManager
{
    
    public Weapon CurrentWeapon;
    
    public Dictionary<Define.WeaponName, Weapon> Weapons { get; private set; } = new Dictionary<Define.WeaponName, Weapon>();
    
    public void InitWeapon()
    {
        Weapons.Add(Define.WeaponName.Sword, CreateWeapon(Define.WeaponName.Sword));
        Weapons.Add(Define.WeaponName.Polearm, CreateWeapon(Define.WeaponName.Polearm));
        Weapons.Add(Define.WeaponName.TwoHander, CreateWeapon(Define.WeaponName.TwoHander));
        SetWeapon(Weapons[Define.WeaponName.Sword]);
    }
    
    public Weapon CreateWeapon(Define.WeaponName weapon)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefabs/{Enum.GetName(typeof(Define.WeaponName), weapon)}");
        GameObject weaponObj = Object.Instantiate(obj, Player.Instance.gripPoint);
        weaponObj.SetActive(false);
        return weaponObj.GetComponent<Weapon>();
    }

    public void SetWeapon(Weapon newWeapon)
    {
        if (newWeapon == CurrentWeapon) return;

        CurrentWeapon = newWeapon;
        CurrentWeapon.gameObject.SetActive(true);
    }

    public void RemoveWeapon()
    {
        if (CurrentWeapon == null) return;

        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = null;
    }

    public void ChangeWeapon(Define.WeaponName nextWeapon)
    {
        if (Weapons.TryGetValue(nextWeapon, out Weapon newWeapon) == false) return;
        if (CurrentWeapon == newWeapon) return;

        RemoveWeapon();
        SetWeapon(newWeapon);

        IState currState = Player.Instance.State.CurrentState;
        if (currState == Player.Instance.State.States[Define.StateName.Idle])
        {
            IdleState idleState = (IdleState)currState;
            idleState.WeaponAnimation(newWeapon);
        }
    }

    public void Attack()
    {
        CurrentWeapon?.Attack();
    }
}