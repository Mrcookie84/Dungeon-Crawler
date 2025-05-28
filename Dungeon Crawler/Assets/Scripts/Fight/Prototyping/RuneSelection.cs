using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.Collections.Unicode;

public class RuneSelection : MonoBehaviour
{
    [Header("Rune UI")]
    [SerializeField] private Transform runeHolder;
    [SerializeField] private Rune[] runeSelectors;

    [Header("Mana")]
    public int maxMana;
    [HideInInspector] public int currentMana;
    private int potentialMana;
    [SerializeField] private Slider manaSliderUI;
    [SerializeField] private Slider potentialManaSliderUI;

    [Header("Stability")]
    public int maxStability;
    [HideInInspector] public int currentStability;
    [SerializeField] private Slider stabilitySliderUI;

    public Dictionary<Rune, int> selectedRunes = new Dictionary<Rune, int>();

    // ========================= Propri�t�s ============================= //

    public string CurrentSpellPlayer
    {
        get { return "Spells/SpellsPlayer/SpellPlayerData" + GetRuneCombinationData(); }
    }

    public string CurrentSpellEnemy
    {
        get { return "Spells/SpellsEnemy/SpellEnemyData" + GetRuneCombinationData(); }
    }

    public bool IsEmpty
    {
        get
        {
            foreach (var count in selectedRunes.Values)
            {
                if (count > 0) return false;
            }

            return true;
        }
    }

    // ========================= M�thodes internes ============================= //

    private void Start()
    {
        // Initialisation des runes s�lectionn�e
        foreach (Rune rune in runeSelectors)
        {
            if (rune == null) continue;
            selectedRunes.Add(rune, 0);
        }

        // Initialisation du mana
        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.maxValue = maxMana;
        manaSliderUI.value = maxMana;
        potentialManaSliderUI.maxValue = maxMana;
        potentialManaSliderUI.value = maxMana;

        // Initialisation de la capacit�
        currentStability = maxStability;

        stabilitySliderUI.maxValue = maxStability;
        stabilitySliderUI.value = maxStability;
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
    
    private void ChangeCurrentStability(int amount)
    {
        currentStability += amount;
        stabilitySliderUI.value = currentStability;
    }

    private bool canUsMoreMana(int amount)
    {
        return potentialMana - amount >= 0;
    }

    private bool canUsMoreStability(int amount)
    {
        return currentStability - amount >= 0;
    }

    private void UpdateRuneUI()
    {
        DeleteAllRuneUI();

        int runeIndex = 0;
        foreach (Rune rune in selectedRunes.Keys)
        {
            // Déterminer l'état
            
            
            // Affichage de la sélection
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

    // ========================= M�thodes externes ============================= //

    public bool TryAddRune(Rune rune)
    {   
        if (CheckRuneConflict(rune) && canUsMoreMana(rune.data.manaCost) && canUsMoreStability(rune.data.manaCost))
        {
            ChangePotentialMana(-rune.data.manaCost);
            ChangeCurrentStability(-rune.data.manaCost);
            selectedRunes[rune] += 1;
            UpdateRuneUI();

            return true;
        }

        return false;
    }

    public void RemoveRune(Rune rune, bool restorMana = true)
    {
        if (restorMana)
            ChangePotentialMana(rune.data.manaCost * selectedRunes[rune]);
        ChangeCurrentStability(rune.data.manaCost * selectedRunes[rune]);

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

            RemoveRune(rune, restoreRune);
        }
    }

    public void ResetMana()
    {
        currentMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.value = maxMana;
        potentialManaSliderUI.value = maxMana;
    }

    public void ResetStability()
    {
        currentStability = maxStability;

        stabilitySliderUI.value = maxStability;
    }
}
