using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    
    [SerializeField] Image attack;
    [SerializeField] Image subAttack;
    [SerializeField] Image attackCool;
    [SerializeField] Image subAttackCool;

    private Dictionary<Define.WeaponName, Sprite> attackIcons = new Dictionary<Define.WeaponName, Sprite>();
    private Dictionary<Define.WeaponName, Sprite> subAttackIcons = new Dictionary<Define.WeaponName, Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        string[] weaponNames = Enum.GetNames(typeof(Define.WeaponName));

        foreach (string weaponName in weaponNames)
        {
            Sprite sprite = Resources.Load<Sprite>($"Icons/{weaponName}");
            attackIcons.Add((Define.WeaponName)Enum.Parse(typeof(Define.WeaponName),weaponName), sprite);
        }
    }

    public void AddSubAttackIcon(Define.WeaponName weapon, string name)
    {
        Sprite sprite = Resources.Load<Sprite>($"Icons/{name}");
        subAttackIcons.Add(weapon, sprite);
    }

    public void ChangeIcon(Define.WeaponName weapon)
    {
        attack.sprite = attackIcons[weapon];
        Sprite subIcon;
        if (subAttackIcons.TryGetValue(weapon, out subIcon))
        {
            subAttack.gameObject.SetActive(true);
            subAttack.sprite = subIcon;
            return;
        }
        subAttack.gameObject.SetActive(false);
    }

    public void StartCoolTime(float time, bool isSub = false)
    {
        StartCoroutine(CoolTimeRoutine(time, isSub));
    }

    IEnumerator CoolTimeRoutine(float time, bool isSub = false)
    {
        Image icon = isSub ? subAttackCool : attackCool;
        icon.fillAmount = 1;
        float timer = 0f;
        while (timer <= time)
        {
            yield return null;
            timer += Time.deltaTime;
            icon.fillAmount = timer / time;
        }
        icon.fillAmount = 0;
    }
}
