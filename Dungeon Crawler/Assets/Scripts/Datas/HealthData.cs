using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObject/HealthData")]
public class HealthData : ScriptableObject
{
    [Header("Info")]
    public int defaultHealth;
    [HideInInspector] public int currentHealth;
    public bool isLinkedToInv;
    public bool keepHealth;
}
