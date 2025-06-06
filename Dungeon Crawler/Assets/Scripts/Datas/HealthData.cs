using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObject/HealthData")]
public class HealthData : ScriptableObject
{
    [Header("Info")]
    public int defaultHealth;
    [HideInInspector] public int currentHealth;
    public bool isLinkedToInv;
    public bool keepHealth;

    [Header("Resistance")]
    public SerializedDictionary<DamageTypesData.DmgTypes, int> resistInfos =
        new SerializedDictionary<DamageTypesData.DmgTypes, int>()
        {
            { DamageTypesData.DmgTypes.Reality , 0},
            { DamageTypesData.DmgTypes.Crush , 0},
            { DamageTypesData.DmgTypes.Contact , 0},
            { DamageTypesData.DmgTypes.Degradation , 0},
            { DamageTypesData.DmgTypes.DimensionalWeakness , 0}
        };
}
