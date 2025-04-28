using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    [SerializeField] private string runeSelectionTag;
    private bool selected = false;

    public void UpdateRuneSelection()
    {
        RuneSelection runeSelection = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<RuneSelection>();
        if (!selected)
        {
            runeSelection.AddRune(gameObject);
            selected = true;
        }
        else
        {
            runeSelection.RemoveRune(gameObject);
            selected = false;
        }
    }
    
    public int GetID()
    {
        return data.ID;
    }
}
