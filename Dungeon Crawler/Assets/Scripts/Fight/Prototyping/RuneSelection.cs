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

    [Header("ManaUI")]
    public int maxMana;
    public int currentMana;
    private int potentialMana;
    [SerializeField] private Slider manaSliderUI;
    [SerializeField] private Slider potentialManaSliderUI;

    [Header("Current State")]
    public List<GameObject> selectedRunes;

    public UnityEvent ResetRune = new UnityEvent();

    private void Start()
    {
        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.maxValue = maxMana;
        manaSliderUI.value = maxMana;
        potentialManaSliderUI.maxValue = maxMana;
        potentialManaSliderUI.value = maxMana;
    }

    public void AddRune(GameObject rune)
    {
        Rune runeComponent = rune.GetComponent<Rune>();
        int runeID = runeComponent.GetID();
        
        if (CheckRuneConflict(runeID) && canUsMoreMana(runeComponent.data.manaCost))
        {
            RemoveMana(runeComponent.data.manaCost);
            selectedRunes.Add(rune);
            runeComponent.selected = true;
        }
        else
        {
            runeComponent.selected = false;
        }

        GetRuneCombinationData();
        
        UpdateRuneUI();
    }

    public void RemoveRune(GameObject rune)
    {
        Rune runeComponent = rune.GetComponent<Rune>();
        RemoveMana(-runeComponent.data.manaCost);

        selectedRunes.Remove(rune);
        runeComponent.selected = false;
        GetRuneCombinationData();
        
        UpdateRuneUI();
    }
    
    private bool CheckRuneConflict(int runeID)
    {
        if (runeID == 2 && IsRuneIDInSelection(3)) return false;
        else if (runeID == 3 && IsRuneIDInSelection(2)) return false;
        else return true;
    }

    // Spells/SpellData
    public string GetRuneCombinationData()
    {   
        string dataPath = $"";
        for (int i = 0; i < 4; i++)
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
            Rune runeData = selectedRunes[i].GetComponent<Rune>();
            int runeID = runeData.GetID();

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
        ResetRune.Invoke();
    }

    private void RemoveMana(int amount)
    {
        potentialMana -= amount;
        potentialManaSliderUI.value = potentialMana;
    }

    public void ResetMana()
    {
        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.value = maxMana;
        potentialManaSliderUI.value = maxMana;
    }

    private bool canUsMoreMana(int amout)
    {
        return potentialMana - amout >= 0;
    }

    private void UpdateRuneUI()
    {
        DeleteAllRuneUI();
        
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
            if (runeHolder.GetChild(i).childCount != 0)
            {
                Destroy(runeHolder.GetChild(i).GetChild(0).gameObject);
            }
        }
    }
}
