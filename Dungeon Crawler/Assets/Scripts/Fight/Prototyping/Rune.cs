using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    [SerializeField] private string runeSelectionTag;
    private RuneSelection runeSelection;
    private int amountSelected = 0;
    public int maxSelected;

    private void Start()
    {
        runeSelection = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<RuneSelection>();
    }

    public void UpdateRuneSelection()
    {
        if (amountSelected < maxSelected)
        {
            if (runeSelection.AddRune(this))
                amountSelected += 1;
        }
        else
        {
            amountSelected = 0;
            for (int i = 0; i < maxSelected; i++)
                runeSelection.RemoveRune(this);
        }
    }
    
    public int GetID()
    {
        return data.ID;
    }

    public void UnSelect()
    {
        amountSelected = 0;
    }
}
