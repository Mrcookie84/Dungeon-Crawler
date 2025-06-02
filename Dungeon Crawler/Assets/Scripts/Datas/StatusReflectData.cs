using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusReflectData", menuName = "ScriptableObject/Status/StatusReflectData")]
public class StatusReflectData : StatusData
{
    [Header("Recharge")]
    public bool recharge;
    [Min(1)] public int chargeTurn;
    public StatusData reloadStatus;

    [Header("Reflect")]
    public bool reflect;

    public override void Apply(GameObject entity, GameObject source)
    {
        base.Apply(entity, source);
        
        EntityHealth health = entity.GetComponent<EntityHealth>();

        health.invicible = true;
    }

    public override void Hit(GameObject entity, EntityHealth.DamageInfo attackInfo)
    {
        EntityHealth health = entity.GetComponent<EntityHealth>();

        health.invicible = false;

        EntityStatusHolder entityStatusHolder = entity.GetComponent<EntityStatusHolder>();
        entityStatusHolder.RemoveStatus(this);

        if (reflect && attackInfo.attacker != null)
        {
            EntityHealth attackerHeatlh = attackInfo.attacker.GetComponent<EntityHealth>();
            
            attackerHeatlh.TakeDamage(null, attackInfo.dmgValue, attackInfo.dmgType);
        }
        
        if (recharge)
        {
            entityStatusHolder.AddStatus(reloadStatus, chargeTurn, null);
        }
    }

    public override void Finish(GameObject entity)
    {
        
    }
}
