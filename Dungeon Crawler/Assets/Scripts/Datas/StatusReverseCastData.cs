using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusReverseCastData", menuName = "ScriptableObject/Status/StatusReverseCastData")]
public class StatusReverseCastData : StatusData
{
    public override void Apply(GameObject entity, GameObject source)
    {
        base.Apply(entity, source);

        EntityPosition posComp = entity.GetComponent<EntityPosition>();
        posComp.reverseLook = true;
    }

    public override void Finish(GameObject entity)
    {
        base.Finish(entity);

        EntityPosition posComp = entity.GetComponent<EntityPosition>();
        posComp.reverseLook = false;
    }
}
