using UnityEngine;
using TMPro;
using System;
using System.ComponentModel.Design.Serialization;

public class KeybindButton : MonoBehaviour
{
    public string actionName;
    public TMP_Text buttonText;
    private KeyCode kc;

    private bool waitingForKey = false;

    private void Start()
    {
        UpdateButtonText();
    }

    private void Update()
    {
        if (waitingForKey)
        {
            foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kc))
                {
                    KeybindManager.SINGLETON.SetKey(actionName, kc);
                    waitingForKey = false;
                    UpdateButtonText();
                    break;
                }
            }
        }
    }

    public void StartKeyMapping()
    {
        waitingForKey = true;
        buttonText.text = "Appuyez...";
    }

    private void UpdateButtonText()
    {
        buttonText.text = KeybindManager.SINGLETON.GetKey(actionName).ToString();
    }
}