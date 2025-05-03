using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellPlayerData", menuName = "ScriptableObject/Spells/SpellPlayerData")]
public class SpellPlayerData : ScriptableObject
{
    public bool switchWorld = false;
    public bool multipleTargets = false;
}