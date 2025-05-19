using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatusHolder : MonoBehaviour
{
    [SerializeField] private StatusData defaultStatus;
    [SerializeField] private int defaultDuration;
    
    private List<StatusInfo> statusList = new List<StatusInfo>();

    private void Start()
    {
        if (defaultStatus == null) return;
        AddStatus(defaultStatus, defaultDuration);
    }

    public void AddStatus(StatusData status, int duration)
    {
        status.Apply(gameObject);
        statusList.Add(new StatusInfo(status,duration));
    }

    public void UpdateStatus()
    {
        for (int i = 0; i < statusList.Count; i++)
        {
            StatusInfo statusInfo = statusList[i];
            statusInfo.status.Tick(gameObject);
            statusInfo.duration -= 1;
            
            
            if (statusInfo.duration <= 0)
            {
                statusInfo.status.Finish(gameObject);
                statusList.RemoveAt(i);
                return;
            }

            statusList[i] = statusInfo;
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
    }
}

