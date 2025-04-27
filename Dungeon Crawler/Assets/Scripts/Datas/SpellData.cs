using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObject/Spells/SpellData")]
public class SpellData : ScriptableObject
{
    public List<Vector2Int> hitCellList = new List<Vector2Int>();
    public List<DamageTypesData> damageTypesData = new List<DamageTypesData>();
    public List<Vector2Int> displacementList = new List<Vector2Int>();

}
