using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EntityStatusHolder;

public class StatusHolderHandler : MonoBehaviour
{
    [SerializeField] private GameObject statusUI;
    [SerializeField] private float xOffSet;
    [SerializeField] private float yOffSet;
    [SerializeField] private float statusPerRow;

    public void ResetStatus()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public void UpdateStatus(List<StatusInfo> statusList)
    {
        ResetStatus();

        float currentXOffSet = 0f;
        float currentYOffSet = 0f;

        foreach (StatusInfo info in statusList)
        {
            if (info.duration <= 0) continue;

            GameObject currentUI = Instantiate(statusUI, transform);
            currentUI.name = info.status.name;
            currentUI.transform.localPosition += new Vector3(currentXOffSet, currentYOffSet, 0f);

            if (info.status.icon != null)
                currentUI.GetComponent<Image>().sprite = info.status.icon;

            if (info.status.permanent)
                currentUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "";
            else
                currentUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = info.duration.ToString();

            currentXOffSet = (currentXOffSet + xOffSet) % (xOffSet * statusPerRow);
            if (currentXOffSet == 0f)
                currentYOffSet += yOffSet;
        }
    } 
}
