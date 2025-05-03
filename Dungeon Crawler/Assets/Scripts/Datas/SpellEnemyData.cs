using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellEnemyData", menuName = "ScriptableObject/Spells/SpellEnemyData")]
public class SpellEnemyData : ScriptableObject
{
    [Header("Zone Data")]
    public List<Vector2Int> hitCellList = new List<Vector2Int>();
    public List<DamageTypesData> damageTypesData = new List<DamageTypesData>();
    public List<Vector2Int> displacementList = new List<Vector2Int>();

    [Header("Barrier Interactions")]
    public bool reinforceBarrier = false;
    public bool weakenBarrier = false;
    public bool blockedByBarrier = false;
}
