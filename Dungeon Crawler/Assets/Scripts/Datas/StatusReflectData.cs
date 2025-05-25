using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusReflectData", menuName = "ScriptableObject/Status/StatusReflectData")]
public class StatusReflectData : StatusData
{
    [Header("Recharge")]
    public bool recharge;
    public int charge;

    public override void Apply(GameObject entity)
    {
        EntityHealth health = entity.GetComponent<EntityHealth>();

        health.invicible = true;
    }

    public override void Hit(GameObject entity)
    {
        EntityHealth health = entity.GetComponent<EntityHealth>();

        health.invicible = false;

        EntityStatusHolder entityStatusHolder = entity.GetComponent<EntityStatusHolder>();
        entityStatusHolder.RemoveStatus(this);

        if (!recharge)
        {
            // Code qui applique le statut cooldown
            // Oh mon dieu les implications, il est trop tard pour que j'y pense
        }
    }

    public override void Finish(GameObject entity)
    {
        
    }
}
