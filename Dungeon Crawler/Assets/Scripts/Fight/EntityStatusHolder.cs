using System;
using System.Collections.Generic;
using UnityEngine;
using static EntityStatusHolder;

public class EntityStatusHolder : MonoBehaviour
{
    [SerializeField] private StatusData[] defaultStatus;
    [SerializeField] private int defaultDuration;

    private GridManager updateGrid;
    
    public List<StatusInfo> statusList = new List<StatusInfo>();

    private void Start()
    {
        updateGrid = GetComponent<EntityPosition>().LinkedGrid;

        foreach (var status in defaultStatus)
            if (status != null)
                AddStatus(status, defaultDuration, null);
    }

    public void AddStatus(StatusData status, int duration, GameObject source)
    {
        for (int i = 0; i < statusList.Count; i++)
        {
            if (statusList[i].status != status) continue;

            if (status.onOff)
                RemoveStatus(status);
            else
                statusList[i].SetDuration(Math.Max(duration, statusList[i].duration));

            return;
        }

        statusList.Add(new StatusInfo(status,duration));
        status.Apply(gameObject, source);

        updateGrid.gridUpdate.Invoke();
    }

    public void RemoveStatus(StatusData status, bool applyFinishedEffect = true)
    {
        List<StatusInfo> newStatusList = new List<StatusInfo>();

        for (int i = 0; i < statusList.Count; i++)
        {
            if (statusList[i].status != status)
            {
                newStatusList.Add(statusList[i]);
                continue;
            }

            Debug.Log(statusList[i].status.name);
            if (applyFinishedEffect) status.Finish(gameObject);
        }

        statusList = newStatusList;
        updateGrid.gridUpdate.Invoke();
    }

    public void UpdateStatus()
    {
        for (int i = 0; i < statusList.Count; i++)
        {
            StatusInfo statusInfo = statusList[i];
            statusInfo.status.Tick(gameObject);

            if (!statusInfo.status.permanent)
                statusInfo.duration -= 1;
            
            if (statusInfo.duration <= 0)
            {
                statusInfo.status.Finish(gameObject);
                RemoveStatus(statusInfo.status);
                continue;
            }

            statusList[i] = statusInfo;
        }

        updateGrid.gridUpdate.Invoke();
    }

    public void DamageResponse(EntityHealth.DamageInfo attackInfo)
    {
        var statusListCopy = new StatusInfo[statusList.Count];
        statusList.CopyTo(statusListCopy);

        foreach (var statusInfo in statusListCopy)
        {
            statusInfo.status.Hit(gameObject, attackInfo);
        }
    }

    public void ForcedUpdate()
    {
        updateGrid.gridUpdate.Invoke();
    }

    [Serializable]
    public struct StatusInfo
    {
        [SerializeField] public StatusData status { get; set; }
        [SerializeField] public int duration { get; set; }

        public StatusInfo(StatusData newStatus, int newDuration)
        {
            this.status = newStatus;
            this.duration = newDuration;
        }

        public void SetDuration(int newDuration)
        {
            this.duration = newDuration;
        }
    }
}

