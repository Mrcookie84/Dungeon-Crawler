using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusReflectData", menuName = "ScriptableObject/Status/StatusReflectData")]
public class StatusReflectData : StatusData
{
    [Header("Recharge")]
    public bool recharge;
    public int chargeTurn;
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
            Debug.Log("Dégats renvoyés");
            EntityHealth attackerHeatlh = attackInfo.attacker.GetComponent<EntityHealth>();
            
            attackerHeatlh.TakeDamage(null, attackInfo.dmgValue);
        }
        
        if (recharge)
        {
            // Code qui applique le statut cooldown
            // Oh mon dieu les implications, il est trop tard pour que j'y pense
        }
    }

    public override void Finish(GameObject entity)
    {
        
    }
}
