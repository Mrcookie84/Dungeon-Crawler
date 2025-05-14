using System.Collections.Generic;
using UnityEngine;

public class EntityStatusHolder : MonoBehaviour
{
    private List<StatusInfo> statusList;

    public void AddStatus(StatusData status, int duration)
    {
        statusList.Add(new StatusInfo(status,duration));
    }

    public void UpdateStatus()
    {
        for (int i = 0; i < statusList.Count; i++)
        {
            StatusInfo statusInfo = statusList[i];
            statusInfo.DecrementDuration();

            if (statusInfo.duration <= 0) statusList.RemoveAt(i);
        }
    }
    
    public struct StatusInfo
    {
        public StatusData status { get; set; }
        public int duration { get; set; }

        public StatusInfo(StatusData newStatus, int newDuration)
        {
            this.status = newStatus;
            this.duration = newDuration;
        }

        public void DecrementDuration()
        {
            this.duration -= 1;
        }
    }
}

