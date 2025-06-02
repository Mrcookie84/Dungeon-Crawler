using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EntityStatusHolder;

public class StatusHolderHandler : MonoBehaviour
{
    [SerializeField] private GameObject statusUI;

    public void ResetStatus(int nbStatus)
    {
        for (int i = 0; i < nbStatus; i++)
        {
            if (transform.GetChild(i).childCount == 0) continue;
            
            DestroyImmediate(transform.GetChild(i).GetChild(0).gameObject);
        }
    }

    public void UpdateStatus(List<StatusInfo> statusList)
    {
        ResetStatus(statusList.Count);

        int statusHolderIndex = 0;
        foreach (StatusInfo info in statusList)
        {
            if (info.duration <= 0) continue;

            Debug.Log($"{name} : {statusHolderIndex}");
            GameObject currentUI = Instantiate(statusUI, transform.GetChild(statusHolderIndex));
            statusHolderIndex++;
            
            currentUI.name = info.status.name;

            if (info.status.icon != null)
                currentUI.GetComponent<Image>().sprite = info.status.icon;

            if (info.status.permanent)
                currentUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "";
            else
                currentUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = info.duration.ToString();
        }
    } 
}
