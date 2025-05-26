using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusSwitchWorldData", menuName = "ScriptableObject/Status/StatusSwitchWorldData")]
public class StatusSwitchWorldData : StatusData
{
    public override void Finish(GameObject entity)
    {
        EntityPosition posComp = entity.GetComponent<EntityPosition>();

        Vector2Int newPos = posComp.gridPos;
        newPos.y = (newPos.y + 1) % 2;
        
        posComp.ChangePosition(newPos);
        posComp.LinkedGrid.UpdateEntitiesIndex();
        posComp.LinkedGrid.gridUpdate.Invoke();
    }
}
