using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusStunData", menuName = "ScriptableObject/Status/StatusStunData")]
public class StatusStunData : StatusData
{
    public override void Apply(GameObject entity, GameObject source)
    {
        base.Apply(entity, source);
        
        EnemyAI ai = entity.GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.lobotomized = true;
        }
    }

    public override void Finish(GameObject entity)
    {
        base.Finish(entity);

        EnemyAI ai = entity.GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.lobotomized = false;
        }
    }
}
