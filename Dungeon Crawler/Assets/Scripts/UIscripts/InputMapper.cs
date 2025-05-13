using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputMapper : MonoBehaviour
{
    private GameObject clickedButton;

    private KeyCode lastHitKey;

    private bool isEditing = false;

    public InputManager inptMgr;

    public void OnButtonClick()
    {
        if (isEditing) return;

        EventSystem currentEvent = EventSystem.current;
        clickedButton = currentEvent.currentSelectedGameObject;
        clickedButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "?";
        isEditing = true;
    }

    private void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyUp)
        {
            lastHitKey = Event.current.keyCode;
            if (!isEditing) return;

            clickedButton.transform.GetChild(0).GetComponent<TMP_Text>().text = lastHitKey.ToString();
            PlayerPrefs.SetString(clickedButton.name, lastHitKey.ToString());
            isEditing = false;
            
            inptMgr.UpdateMapping();

        }
    }
}
