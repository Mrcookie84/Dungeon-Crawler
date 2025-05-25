using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntityStatusHolder;

public class StatusHolderGroupManager : MonoBehaviour
{
    [SerializeField] private GridManager linkedGrid;

    [SerializeField] private StatusHolderHandler[] statusHolderHandlers = new StatusHolderHandler[6];
    public void UpdateStatusHolders()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject currentEntity = linkedGrid.entityList[i];
            if (currentEntity == null) continue;

            List<StatusInfo> statusInfos = currentEntity.GetComponent<EntityStatusHolder>().statusList;

            statusHolderHandlers[i].UpdateStatus(statusInfos);
        }
    }
}
