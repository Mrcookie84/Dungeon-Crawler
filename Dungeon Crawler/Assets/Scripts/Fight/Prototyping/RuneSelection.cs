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
    public Dictionary<Rune, int> selectedRunes = new Dictionary<Rune, int>();

    // ========================= Propriétés ============================= //

    public string CurrentSpellPlayer
    {
        get { return "Spells/SpellPlayerData" + GetRuneCombinationData(); }
    }

    public string CurrentSpellEnemy
    {
        get { return "Spells/SpellEnemyData" + GetRuneCombinationData(); }
    }

    // ========================= Méthodes internes ============================= //

    private void Start()
    {
        // Initialisation des runes sélectionnée
        foreach (Rune rune in runeSelectors)
        {
            if (rune == null) continue;
            selectedRunes.Add(rune, 0);
        }

        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.maxValue = maxMana;
        manaSliderUI.value = maxMana;
        potentialManaSliderUI.maxValue = maxMana;
        potentialManaSliderUI.value = maxMana;
    }

    private bool CheckRuneConflict(Rune newRune)
    {
        // Conflit entre focus et extension
        if (newRune == runeSelectors[3] && selectedRunes[runeSelectors[4]] > 0)
            return false;
        // Conflit entre extension et focus
        else if (newRune == runeSelectors[4] && selectedRunes[runeSelectors[3]] > 0)
            return false;
        // Pas de conflit
        else
            return true;
    }

    private void ChangeCurrentMana(int amount)
    {
        currentMana += amount;
        manaSliderUI.value = currentMana;
    }

    private void ChangePotentialMana(int amount)
    {
        potentialMana += amount;
        potentialManaSliderUI.value = potentialMana;
    }

    private bool canUsMoreMana(int amount)
    {
        return potentialMana - amount >= 0;
    }

    private void UpdateRuneUI()
    {
        DeleteAllRuneUI();

        int runeIndex = 0;
        foreach (Rune rune in selectedRunes.Keys)
        {
            GameObject runeUI = rune.data.UIPrefab;

            for (int i = 0; i < selectedRunes[rune]; i++)
            {
                Transform slot = runeHolder.GetChild(runeIndex);

                Instantiate(runeUI, slot);

                runeIndex++;
            }
        }
    }

    private void DeleteAllRuneUI()
    {
        for (int i = 0; i < runeHolder.childCount; i++)
        {
            if (runeHolder.GetChild(i).childCount == 0) continue;

            DestroyImmediate(runeHolder.GetChild(i).GetChild(0).gameObject);
        }
    }

    // ========================= Méthodes externes ============================= //

    public bool TryAddRune(Rune rune)
    {   
        if (CheckRuneConflict(rune) && canUsMoreMana(rune.data.manaCost))
        {
            ChangePotentialMana(-rune.data.manaCost);
            selectedRunes[rune] += 1;
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
        ChangePotentialMana(rune.data.manaCost * selectedRunes[rune]);

        selectedRunes[rune] = 0;
        
        UpdateRuneUI();
    }
    
    public string GetRuneCombinationData()
    {   
        string dataPath = $"";
        foreach (Rune rune in runeSelectors)
        {
            if (rune == null) continue;
            for (int i = 0; i < selectedRunes[rune]; i++)
            {
                dataPath += $"{rune.Id}";
            }
        }

        return dataPath;
    }

    public void UpdateMana()
    {
        currentMana = potentialMana;
        manaSliderUI.value = potentialMana;
        potentialManaSliderUI.value = potentialMana;

        DeleteAllRuneUI();
    }

    public void ResetSelection(bool restoreRune = false)
    {
        foreach (Rune rune in runeSelectors)
        {
            if (rune == null) continue;

            if (restoreRune)
            {
                rune.RestoreRune(selectedRunes[rune]);
            }

            selectedRunes[rune] = 0;
        }

        UpdateRuneUI();
    }

    public void ResetMana()
    {
        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.value = maxMana;
        potentialManaSliderUI.value = maxMana;
    }
}
