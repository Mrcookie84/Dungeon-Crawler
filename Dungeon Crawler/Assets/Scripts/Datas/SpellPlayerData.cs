using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellPlayerData", menuName = "ScriptableObject/Spells/SpellPlayerData")]
public class SpellPlayerData : ScriptableObject
{
    public bool switchWorld = false;
    public bool multipleTargets = false;
    public DamageTypesData.DmgTypes type;

    [Header("Change State")]
    public StatusData appliedStatus;
    public int statusDuration;
    [Space(5)]
    public int healAmount;

    [Header("Timer")]
    public float t_fx;
    public float t_effect;

    [Header("Description")]
    public string desc;

    [Header("FX")]
    public GameObject fxCell;

    public float SpellDuration
    {
        get { return Mathf.Max(t_effect, t_fx); }
    }
}