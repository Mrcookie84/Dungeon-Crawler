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
    private int interMana;
    private int potentialMana;
    [SerializeField] private Slider manaSliderUI;
    [SerializeField] private Slider interManaSliderUI;
    [SerializeField] private Slider potentialManaSliderUI;

    [Header("Stability")]
    public int maxStability;
    [HideInInspector] public int currentStability;
    private int interStability;
    [SerializeField] private Slider stabilitySliderUI;
    [SerializeField] private Slider interStabilitySliderUI;

    public Dictionary<Rune, int> selectedRunes = new Dictionary<Rune, int>();

    // ========================= Propri�t�s ============================= //
    public int CurrentMana
    {
        get { return currentMana; }
        set
        {
            currentMana = value;
            manaSliderUI.value = currentMana;
        }
    }

    private int InterMana
    {
        get { return interMana; }
        set
        {
            interMana = value;
            interManaSliderUI.value = interMana;
        }
    }

    private int PotentialMana
    {
        get { return potentialMana; }
        set
        {
            potentialMana = value;
            potentialManaSliderUI.value = potentialMana;
        }
    }

    public int CurrentStability
    {
        get { return currentStability; }
        set
        {
            currentStability = value;
            stabilitySliderUI.value = currentStability;
        }
    }

    private int InterStability
    {
        get { return interStability; }
        set
        {
            interStability = value;
            interStabilitySliderUI.value = interStability;
        }
    }

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

    private void Awake()
    {
        // Initialisation des runes s�lectionn�e
        foreach (Rune rune in runeSelectors)
        {
            if (rune == null) continue;
            selectedRunes.Add(rune, 0);
        }

        // Initialisation du mana
        currentMana = maxMana;
        interMana = maxMana;
        potentialMana = maxMana;

        manaSliderUI.maxValue = maxMana;
        manaSliderUI.value = maxMana;
        interManaSliderUI.maxValue = maxMana;
        interManaSliderUI.value = maxMana;
        potentialManaSliderUI.maxValue = maxMana;
        potentialManaSliderUI.value = maxMana;

        // Initialisation de la stabilité
        currentStability = maxStability;
        interStability = maxStability;

        stabilitySliderUI.maxValue = maxStability;
        stabilitySliderUI.value = maxStability;
        interStabilitySliderUI.maxValue = maxStability;
        interStabilitySliderUI.value = maxStability;
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

    private bool canUsMoreMana(int amount)
    {
        return interMana - amount >= 0;
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
            // Mise à jour du bouton
            rune.UpdateUI();
            
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
            PotentialMana -= rune.data.manaCost;
            InterMana -= rune.data.manaCost;

            CurrentStability -= rune.data.manaCost;
            InterStability -= rune.data.manaCost;

            selectedRunes[rune] += 1;
            UpdateRuneUI();

            return true;
        }

        return false;
    }

    public void RemoveRune(Rune rune, bool restorMana = true)
    {
        if (restorMana)
        {
            PotentialMana += rune.data.manaCost * selectedRunes[rune];
            InterMana += rune.data.manaCost * selectedRunes[rune];
        }
        CurrentStability += rune.data.manaCost * selectedRunes[rune];
        InterStability += rune.data.manaCost * selectedRunes[rune];

        selectedRunes[rune] = 0;
        
        UpdateRuneUI();
    }

    public void PreviewRune(RuneData rune)
    {
        PotentialMana -= rune.manaCost;
        InterStability -= rune.manaCost;
    }

    public void UnPreviewRune(RuneData rune)
    {
        PotentialMana += rune.manaCost;
        InterStability += rune.manaCost;
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
        CurrentMana = potentialMana;

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
        CurrentMana = maxMana;
        InterMana = maxMana;
        PotentialMana = maxMana;
    }

    public void ResetStability()
    {
        CurrentStability = maxStability;
        InterStability = maxStability;
    }
}
