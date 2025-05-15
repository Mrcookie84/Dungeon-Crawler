using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusRegenData", menuName = "ScriptableObject/Status/StatusRegenData")]
public class StatusRegenData : StatusData
{
    [SerializeField] private int regenAmount = 0;
    
    public override void Tick(GameObject entity)
    {
        Debug.Log("Regen active");
        EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
        
        entityHealth.Heal(regenAmount);
    }
}
