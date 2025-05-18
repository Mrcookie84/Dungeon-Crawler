using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyActionData", menuName = "ScriptableObject/EnemyAction/EnemyActionData")]
public class EnemyActionData : ScriptableObject
{
    [Header("Action Type")]
    public bool isAttack;

    [Header("Attack")]
    public int dmgValue;
    public DamageTypesData.DmgTypes dmgType;
    public bool multipleTarget;

    [Header("Support")]
    public int healAmount;
    public EnemySupportTarget supportTarget;

    [Header("Status applied")]
    public StatusData status;
    public int statusDuration;

    public enum EnemySupportTarget
    {
        Self,
        Other,
        SelfOrOther,
        Multiple
    }
}
