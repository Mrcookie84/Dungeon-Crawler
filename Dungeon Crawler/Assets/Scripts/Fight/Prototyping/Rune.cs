using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    [SerializeField] private string runeSelectionTag;
    private RuneSelection runeSelection;
    public bool selected = false;

    private void Start()
    {
        runeSelection = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<RuneSelection>();
        runeSelection.ResetRune.AddListener(UnSelect);
    }

    public void UpdateRuneSelection()
    {
        if (!selected)
        {
            runeSelection.AddRune(gameObject);
        }
        else
        {
            runeSelection.RemoveRune(gameObject);
        }
    }
    
    public int GetID()
    {
        return data.ID;
    }

    private void UnSelect()
    {
        selected = false;
    }
}
