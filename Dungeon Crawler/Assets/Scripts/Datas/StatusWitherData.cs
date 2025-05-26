using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusWitherData", menuName = "ScriptableObject/Status/StatusWitherData")]
public class StatusWitherData : StatusData
{
    [SerializeField] private int witherAmount = 0;
    [SerializeField] private DamageTypesData.DmgTypes dmgType;
    
    public override void Tick(GameObject entity)
    {
        Debug.Log("Regen active");
        EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
        
        entityHealth.TakeDamage(null, witherAmount);
    }
}
