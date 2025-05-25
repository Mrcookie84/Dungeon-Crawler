using System;
using System.Collections.Generic;
using UnityEngine;
using static EntityStatusHolder;

public class EntityStatusHolder : MonoBehaviour
{
    [SerializeField] private StatusData[] defaultStatus;
    [SerializeField] private int defaultDuration;

    private GridManager updateGrid;
    
    [HideInInspector] public List<StatusInfo> statusList = new List<StatusInfo>();

    private void Start()
    {
        updateGrid = GetComponent<EntityPosition>().LinkedGrid;

        foreach (var status in defaultStatus)
            if (status != null)
                AddStatus(status, defaultDuration);
    }

    public void AddStatus(StatusData status, int duration)
    {
        foreach (var statusInfo in statusList)
        {
            if (statusInfo.status != status) continue;

            statusInfo.duration = duration;
            return;
        }

        status.Apply(gameObject);
        statusList.Add(new StatusInfo(status,duration));

        updateGrid.gridUpdate.Invoke();
    }

    public void RemoveStatus(StatusData status)
    {
        for (int i = 0; i < statusList.Count; i++)
        {
            if (statusList[i].status != status) continue;

            statusList.RemoveAt(i);
            return;
        }
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

        updateGrid.gridUpdate.Invoke();
    }

    public void DamageResponse()
    {
        foreach (var statusInfo in statusList)
        {
            statusInfo.status.Hit(gameObject);
        }
    }
    
    public class StatusInfo
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

