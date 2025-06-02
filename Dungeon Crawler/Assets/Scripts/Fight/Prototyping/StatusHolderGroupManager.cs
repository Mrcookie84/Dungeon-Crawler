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
            if (statusHolderHandlers[i] == null) continue;
            
            GameObject currentEntity = linkedGrid.entityList[i];
            if (currentEntity == null)
            {
                statusHolderHandlers[i].ResetStatus();
                continue;
            }

            List<StatusInfo> statusInfos = currentEntity.GetComponent<EntityStatusHolder>().statusList;
            if (statusInfos.Count == 0)
            {
                statusHolderHandlers[i].ResetStatus();
            }
            else
            {
                Debug.Log($"{currentEntity.name} : status update");
                statusHolderHandlers[i].UpdateStatus(statusInfos);
                Debug.Log($"{currentEntity.name} : status update finished");
            }
        }
    }
}
