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

    [Header("Timer")]
    public float t_fx;
    public float t_damage;
    public float t_disp;
    public float t_barrier;
    
    [Header("Description")]
    public string desc;

    public float SpellDuration
    {
        get { return Mathf.Max(t_barrier, t_damage, t_disp, t_fx); }
    }
}
