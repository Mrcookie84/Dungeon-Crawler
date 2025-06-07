using System.Collections;
using System.Collections.Generic;
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

    [Header("Description")]
    public string desc;
}