using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using static Unity.Collections.Unicode;

public class RuneSelection : MonoBehaviour
{
    public List<GameObject> selectedRunes;

    public void AddRune(GameObject rune)
    {
        Rune runeData = rune.GetComponent<Rune>();
        int runeID = runeData.GetID();
        
        if (CheckRuneConflict(runeID))
        {
            selectedRunes.Add(rune);
        }

        GetRuneCombinationData();
    }

    private bool CheckRuneConflict(int runeID)
    {
        return true;
    }

    public string GetRuneCombinationData()
    {
        string dataPath = $"Spells/SpellData";
        for (int i = 0; i < 4; i++)
        {
            if (IsRuneIDInSelection(i))
            {
                dataPath += $"{i}";
            }
        }

        Debug.Log(dataPath);
        return dataPath;
    }

    private bool IsRuneIDInSelection(int id)
    {
        int[] IDs = GetAllRuneIDs();

        bool result = false;
        for (int i = 0; i < IDs.Length; i++)
        {
            if (IDs[i] == id)
            {
                result = true;
            }
        }

        return result;
    }

    private int[] GetAllRuneIDs()
    {
        int[] idList = new int[selectedRunes.Count];

        for (int i = 0; i < selectedRunes.Count; i++)
        {
            Rune runeData = selectedRunes[i].GetComponent<Rune>();
            int runeID = runeData.GetID();

            idList[i] = runeID;
        }

        return idList;
    }
}
