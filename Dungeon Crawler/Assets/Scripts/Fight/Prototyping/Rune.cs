using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    [SerializeField] private string runeSelectionTag;

    public void AddSelfToRuneSelection()
    {
        Debug.Log("Rune clickée");
        RuneSelection runeSelection = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<RuneSelection>();
        runeSelection.AddRune(gameObject);
    }
    
    public int GetID()
    {
        return data.ID;
    }
}
