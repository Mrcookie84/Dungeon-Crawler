using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectData : MonoBehaviour
{
    [Header("Added runes")]
    public SerializedDictionary<RuneType, int> runes;

    [Header("Stats Boosts")]
    public int addedPV;
    public int addedStability;
    public int addedMana;
    [Space(10)]
    public SerializedDictionary<DamageTypesData.DmgTypes, int> dmgResistance;
    public SerializedDictionary<DamageTypesData.DmgTypes, int> dmgBoost;

    public enum RuneType
    {
        Reality = 0,
        Space = 1,
        Time = 2,
        Focus = 3,
        Extension = 4,
        Affliction = 5
    }
}
