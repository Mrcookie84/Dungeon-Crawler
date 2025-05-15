using UnityEngine;

[CreateAssetMenu(fileName = "DamageTypesData", menuName = "ScriptableObject/Spells/DamageTypesData")]
public class DamageTypesData : ScriptableObject
{
    public DmgTypes[] dmgTypeName;
    public int[] dmgValues;
    
    public enum DmgTypes
    {
        Reality,
        Crush,
        Contact,
        Degradation,
        DimensionalWeakness
    }
}
