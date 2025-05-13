using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode leftKeycode;
    public KeyCode rightKeycode;
    void Start()
    {
        UpdateMapping();
    }
    
    void Update()
    {
        if (Input.GetKeyUp(leftKeycode))
        {
            Debug.Log("aller a gauche avec la touche " + leftKeycode);
        }

        if (Input.GetKeyUp(rightKeycode))
        {
            Debug.Log("aller a droite avec la touche " + rightKeycode);
        }
    }

    public void UpdateMapping()
    {
        string leftKeyText = PlayerPrefs.GetString("LeftAction", "Q");
        leftKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), leftKeyText);

        string rightKeyText = PlayerPrefs.GetString("RightAction", "D");
        rightKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), rightKeyText);
    }
    
}
