using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusRegenData : StatusData
{
    [SerializeField] private int regenAmount = 0;
    
    public override void Tick(GameObject entity)
    {
        EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
        
        entityHealth.Heal(regenAmount);
    }

    public override void Finish()
    {
        return;
    }
}
