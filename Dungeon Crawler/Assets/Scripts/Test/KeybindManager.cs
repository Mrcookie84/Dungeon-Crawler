using UnityEngine;
using System;
using System.Collections.Generic;

public class KeybindManager : MonoBehaviour
{
    public static KeybindManager SINGLETON;

    private Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        if (SINGLETON == null)
        {
            SINGLETON = this;
            DontDestroyOnLoad(gameObject);
            LoadAllKeybinds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetKey(string action, KeyCode key)
    {
        keybinds[action] = key;
        PlayerPrefs.SetString(action, key.ToString());
        PlayerPrefs.Save();
    }

    public KeyCode GetKey(string action)
    {
        if (keybinds.TryGetValue(action, out KeyCode key))
        {
            return key;
        }
        else
        {
            Debug.LogWarning("Action inconnue : " + action);
            return KeyCode.None;
        }
    }

    private void LoadAllKeybinds()
    {
        LoadKey("Right", KeyCode.D);
        LoadKey("Left", KeyCode.Q);
        LoadKey("Grimoire", KeyCode.G);
        LoadKey("Inventaire", KeyCode.I);
        LoadKey("Parametre", KeyCode.Escape);
        LoadKey("Map", KeyCode.M);
        LoadKey("Interagir", KeyCode.F);
        LoadKey("Renard", KeyCode.Keypad1);
        LoadKey("Grenouille", KeyCode.Keypad2);
    }

    private void LoadKey(string action, KeyCode defaultKey)
    {
        string keyString = PlayerPrefs.GetString(action, defaultKey.ToString());
        if (Enum.TryParse(keyString, out KeyCode keyCode))
        {
            keybinds[action] = keyCode;
        }
        else
        {
            keybinds[action] = defaultKey;
        }
    }
}