using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EntityStatusHolder;

public class StatusHolderHandler : MonoBehaviour
{
    [SerializeField] private GameObject statusUI;

    public void ResetStatus()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().enabled = false;
            if (transform.GetChild(i).childCount == 0) continue;
            
            DestroyImmediate(transform.GetChild(i).GetChild(0).gameObject);
        }
    }

    public void UpdateStatus(List<StatusInfo> statusList)
    {
        ResetStatus();
        
        int statusHolderIndex = 0;
        foreach (StatusInfo info in statusList)
        {
            if (info.duration <= 0) continue;

            transform.GetChild(statusHolderIndex).GetComponent<Image>().enabled = true;
            GameObject currentUI = Instantiate(statusUI, transform.GetChild(statusHolderIndex));

            currentUI.transform.localScale = new Vector3(1f, transform.parent.parent.localScale.y, 1f);
            statusHolderIndex++;
            
            currentUI.name = info.status.name;

            if (info.status.icon != null)
            {
                Image statusImage = currentUI.transform.GetChild(0).GetComponent<Image>();
                statusImage.sprite = info.status.icon;
                statusImage.SetNativeSize();
            }

            if (info.status.permanent)
                currentUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "";
            else
                currentUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = info.duration.ToString();
            
            //Debug.Log("yis");
        }
    } 
}
