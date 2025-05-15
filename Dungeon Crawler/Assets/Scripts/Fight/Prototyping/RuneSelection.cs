using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.Collections.Unicode;

public class RuneSelection : MonoBehaviour
{
    [Header("Rune UI")]
    [SerializeField] private Transform runeHolder;
    [SerializeField] private Rune[] runeSelectors;

    [Header("ManaUI")]
    public int maxMana;
    public int currentMana;
    private int potentialMana;
    [SerializeField] private Slider manaSliderUI;
    [SerializeField] private Slider potentialManaSliderUI;

    [Header("Current State")]
    public List<Rune> selectedRunes;

    private void Start()
    {
        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.maxValue = maxMana;
        manaSliderUI.value = maxMana;
        potentialManaSliderUI.maxValue = maxMana;
        potentialManaSliderUI.value = maxMana;
    }

    public bool AddRune(Rune rune)
    {
        Debug.Log(rune.name);
        int runeID = rune.GetID();
        
        if (CheckRuneConflict(runeID) && canUsMoreMana(rune.data.manaCost))
        {
            RemoveMana(rune.data.manaCost);
            selectedRunes.Add(rune);
            UpdateRuneUI();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveRune(Rune rune)
    {
        RemoveMana(-rune.data.manaCost);

        //selectedRunes.Remo(rune);
        for (int i = selectedRunes.Count - 1; i >= 0; i--)
        {
            if (selectedRunes[i] == rune)
            {
                selectedRunes.RemoveAt(i);
                break;
            }
        }
        
        //GetRuneCombinationData();
        
        UpdateRuneUI();
    }
    
    private bool CheckRuneConflict(int runeID)
    {
        if (runeID == 2 && IsRuneIDInSelection(3)) return false;
        else if (runeID == 3 && IsRuneIDInSelection(2)) return false;
        else return true;
    }
    
    public string GetRuneCombinationData()
    {   
        string dataPath = $"";
        for (int i = 0; i < 6; i++)
        {
            if (IsRuneIDInSelection(i))
            {
                dataPath += $"{i}";
            }
        }

        return dataPath;
    }

    public string GetEnemySpellData()
    {
        return "Spells/SpellEnemyData" + GetRuneCombinationData();
    }
    public string GetPlayerSpellData()
    {
        return "Spells/SpellPlayerData" + GetRuneCombinationData();
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
            int runeID = selectedRunes[i].GetID();

            idList[i] = runeID;
        }

        return idList;
    }

    public void UpdateMana()
    {
        currentMana = potentialMana;
        manaSliderUI.value = potentialMana;
        potentialManaSliderUI.value = potentialMana;

        selectedRunes.Clear();
        DeleteAllRuneUI();
    }

    private void RemoveMana(int amount)
    {
        potentialMana -= amount;
        potentialManaSliderUI.value = potentialMana;
    }

    public void ResetSpell()
    {
        foreach (Rune rune in runeSelectors)
        {
            rune.UnSelect();
        }
        
        selectedRunes.Clear();
        UpdateRuneUI();
    }

    public void ResetMana()
    {
        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.value = maxMana;
        potentialManaSliderUI.value = maxMana;
    }

    private bool canUsMoreMana(int amount)
    {
        return potentialMana - amount >= 0;
    }

    private void UpdateRuneUI()
    {
        DeleteAllRuneUI();
        
        Debug.Log(selectedRunes.Count);
        for (int i = 0; i < selectedRunes.Count; i++)
        {
            GameObject runeUI = selectedRunes[i].GetComponent<Rune>().data.UIPrefab;
            Transform slot = runeHolder.GetChild(i);
            
            Instantiate(runeUI, slot);
        }
    }

    private void DeleteAllRuneUI()
    {
        for (int i = 0; i < runeHolder.childCount; i++)
        {
            if (runeHolder.GetChild(i).childCount == 0) continue;
            
            Debug.Log($"Suppressing slot{i}");
            DestroyImmediate(runeHolder.GetChild(i).GetChild(0).gameObject);
        }
    }
}
