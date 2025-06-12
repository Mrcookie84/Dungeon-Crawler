<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneSelection : MonoBehaviour
{
    public static RuneSelection Instance;

    [Header("Rune UI")]
    [SerializeField] private Transform runeHolder;
    [SerializeField] private Rune[] runeSelectors;

    [Header("Mana")]
    public int defaultMana;
    public static int maxMana;
    public static int currentMana;
    private static int interMana;
    private static int potentialMana;
    [SerializeField] private VerticalLayoutGroup manaGraduationGroup;
    [SerializeField] private GameObject manaGrad;
    [SerializeField] private Slider manaSliderUI;
    [SerializeField] private Slider interManaSliderUI;
    [SerializeField] private Slider potentialManaSliderUI;

    [Header("Stability")]
    public int defaultStability;
    public static int maxStability;
    public static int currentStability;
    private static int interStability;
    [SerializeField] private HorizontalLayoutGroup stabilityGraduationGroup;
    [SerializeField] private GameObject stabilityGrad;
    [SerializeField] private Slider stabilitySliderUI;
    [SerializeField] private Slider interStabilitySliderUI;

    public static Dictionary<Rune, int> selectedRunes = new Dictionary<Rune, int>();

    // ========================= Propri�t�s ============================= //
    public static int CurrentMana
    {
        get { return currentMana; }
        set
        {
            currentMana = value;
            Instance.manaSliderUI.value = currentMana;
        }
    }

    private static int InterMana
    {
        get { return interMana; }
        set
        {
            interMana = value;
            Instance.interManaSliderUI.value = interMana;
        }
    }

    private static int PotentialMana
    {
        get { return potentialMana; }
        set
        {
            potentialMana = value;
            Instance.potentialManaSliderUI.value = potentialMana;
        }
    }

    public static int CurrentStability
    {
        get { return currentStability; }
        set
        {
            currentStability = value;
            Instance.stabilitySliderUI.value = currentStability;
        }
    }

    private static int InterStability
    {
        get { return interStability; }
        set
        {
            interStability = value;
            Instance.interStabilitySliderUI.value = interStability;
        }
    }

    public static string CurrentSpellPlayer
    {
        get { return "Spells/SpellsPlayer/SpellPlayerData" + GetRuneCombinationData(); }
    }

    public static string CurrentSpellEnemy
    {
        get { return "Spells/SpellsEnemy/SpellEnemyData" + GetRuneCombinationData(); }
    }

    public static bool IsEmpty
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
        Instance = this;
        
        // Initialisation des runes s�lectionn�e
        foreach (Rune rune in Instance.runeSelectors)
        {
            if (rune == null) continue;
            selectedRunes.Add(rune, 0);
        }
    }

    private void Start()
    {

    }

    public static void InitializeStats()
    {
        maxMana = Instance.defaultMana;
        maxStability = Instance.defaultStability;

        maxMana += PlayerInventory.ManaBoost;
        maxStability += PlayerInventory.StabilityBoost;

        foreach (var rune in Instance.runeSelectors)
        {
            rune.maxRunes = rune.defaultRunes + PlayerInventory.GetBonusRunes(rune.Id);
            rune.InitializeRune();
        }
        

        // Initialisation du mana
        currentMana = maxMana;
        interMana = maxMana;
        potentialMana = maxMana;

        Instance.manaSliderUI.maxValue = maxMana;
        Instance.manaSliderUI.value = maxMana;
        Instance.interManaSliderUI.maxValue = maxMana;
        Instance.interManaSliderUI.value = maxMana;
        Instance.potentialManaSliderUI.maxValue = maxMana;
        Instance.potentialManaSliderUI.value = maxMana;

        // Initialisation de la stabilité
        currentStability = maxStability;
        interStability = maxStability;

        Instance.stabilitySliderUI.maxValue = maxStability;
        Instance.stabilitySliderUI.value = maxStability;
        Instance.interStabilitySliderUI.maxValue = maxStability;
        Instance.interStabilitySliderUI.value = maxStability;
        
        // Plaçage des graduations
        // Mana
        foreach (Transform grad in Instance.manaGrad.transform)
        {
            Destroy(grad.gameObject);
        }

        for (int i = 0; i < maxMana - 1; i++)
        {
            Instantiate(Instance.manaGrad, Instance.manaGraduationGroup.transform);
        }
        float manaHeight = Instance.manaGraduationGroup.GetComponent<RectTransform>().rect.height;
        float manaSpacing = manaHeight / maxMana;
        manaSpacing -= Instance.manaGrad.GetComponent<RectTransform>().rect.height;

        Instance.manaGraduationGroup.spacing = manaSpacing;

        // Stability
        foreach (Transform grad in Instance.stabilityGrad.transform)
        {
            Destroy(grad.gameObject);
        }

        for (int i = 0; i < maxStability - 1; i++)
        {
            Instantiate(Instance.stabilityGrad, Instance.stabilityGraduationGroup.transform);
        }
        float stabilityHeight = Instance.stabilityGraduationGroup.GetComponent<RectTransform>().rect.width;
        float stabilitySpacing = stabilityHeight / maxStability;
        stabilitySpacing -= Instance.stabilityGrad.GetComponent<RectTransform>().rect.width;

        Instance.stabilityGraduationGroup.spacing = stabilitySpacing;
    }

    private static bool canUsMoreMana(int amount)
    {
        return interMana - amount >= 0;
    }

    private static bool canUsMoreStability(int amount)
    {
        return currentStability - amount >= 0;
    }

    private static void UpdateRuneUI()
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
                Transform slot = Instance.runeHolder.GetChild(runeIndex);

                Instantiate(runeUI, slot);

                runeIndex++;
            }
        }
    }

    private static void DeleteAllRuneUI()
    {
        for (int i = 0; i < Instance.runeHolder.childCount; i++)
        {
            if (Instance.runeHolder.GetChild(i).childCount == 0) continue;

            DestroyImmediate(Instance.runeHolder.GetChild(i).GetChild(0).gameObject);
        }
    }

    // ========================= M�thodes externes ============================= //

    public static bool TryAddRune(Rune rune)
    {   
        if (canUsMoreMana(rune.data.manaCost) && canUsMoreStability(rune.data.manaCost))
        {
            PotentialMana -= rune.data.manaCost;
            InterMana -= rune.data.manaCost;

            CurrentStability -= rune.data.manaCost;
            InterStability -= rune.data.manaCost;

            selectedRunes[rune] += 1;
            UpdateRuneUI();

            SpellCaster.ChangeSpell();
            return true;
        }

        return false;
    }

    public static void RemoveRune(Rune rune, bool restorMana = true)
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

    public static void PreviewRune(RuneData rune)
    {
        PotentialMana -= rune.manaCost;
        InterStability -= rune.manaCost;
    }

    public static void UnPreviewRune(RuneData rune)
    {
        PotentialMana += rune.manaCost;
        InterStability += rune.manaCost;
    }
    
    public static string GetRuneCombinationData(Rune projectedRune = null)
    {   
        string dataPath = $"";
        foreach (Rune rune in Instance.runeSelectors)
        {
            if (rune == null) continue;

            for (int i = 0; i < selectedRunes[rune]; i++)
            {
                dataPath += $"{rune.Id}";
            }

            if (rune == projectedRune)
                dataPath += $"{projectedRune.Id}";
        }

        return dataPath;
    }

    public static void UpdateMana()
    {
        CurrentMana = potentialMana;

        DeleteAllRuneUI();
    }

    public static void ResetSelection(bool restoreRune = false)
    {
        foreach (Rune rune in Instance.runeSelectors)
        {
            if (rune == null) continue;

            if (restoreRune)
            {
                rune.RestoreRune(selectedRunes[rune]);
            }

            RemoveRune(rune, restoreRune);
        }
    }

    public static void ResetMana()
    {
        CurrentMana = maxMana;
        InterMana = maxMana;
        PotentialMana = maxMana;
    }

    public static void ResetStability()
    {
        CurrentStability = maxStability;
        InterStability = maxStability;
    }
}
=======
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneSelection : MonoBehaviour
{
    public static RuneSelection Instance;

    [Header("Rune UI")]
    [SerializeField] private Transform runeHolder;
    [SerializeField] private Rune[] runeSelectors;

    [Header("Mana")]
    public int defaultMana;
    public static int maxMana;
    public static int currentMana;
    private static int interMana;
    private static int potentialMana;
    [SerializeField] private VerticalLayoutGroup manaGraduationGroup;
    [SerializeField] private GameObject manaGrad;
    [SerializeField] private Slider manaSliderUI;
    [SerializeField] private Slider interManaSliderUI;
    [SerializeField] private Slider potentialManaSliderUI;

    [Header("Stability")]
    public int defaultStability;
    public static int maxStability;
    public static int currentStability;
    private static int interStability;
    [SerializeField] private HorizontalLayoutGroup stabilityGraduationGroup;
    [SerializeField] private GameObject stabilityGrad;
    [SerializeField] private Slider stabilitySliderUI;
    [SerializeField] private Slider interStabilitySliderUI;

    public static Dictionary<Rune, int> selectedRunes = new Dictionary<Rune, int>();

    // ========================= Propri�t�s ============================= //
    public static int CurrentMana
    {
        get { return currentMana; }
        set
        {
            currentMana = value;
            Instance.manaSliderUI.value = currentMana;
        }
    }

    private static int InterMana
    {
        get { return interMana; }
        set
        {
            interMana = value;
            Instance.interManaSliderUI.value = interMana;
        }
    }

    private static int PotentialMana
    {
        get { return potentialMana; }
        set
        {
            potentialMana = value;
            Instance.potentialManaSliderUI.value = potentialMana;
        }
    }

    public static int CurrentStability
    {
        get { return currentStability; }
        set
        {
            currentStability = value;
            Instance.stabilitySliderUI.value = currentStability;
        }
    }

    private static int InterStability
    {
        get { return interStability; }
        set
        {
            interStability = value;
            Instance.interStabilitySliderUI.value = interStability;
        }
    }

    public static string CurrentSpellPlayer
    {
        get { return "Spells/SpellsPlayer/SpellPlayerData" + GetRuneCombinationData(); }
    }

    public static string CurrentSpellEnemy
    {
        get { return "Spells/SpellsEnemy/SpellEnemyData" + GetRuneCombinationData(); }
    }

    public static bool IsEmpty
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
        Instance = this;
        
        // Initialisation des runes s�lectionn�e
        foreach (Rune rune in Instance.runeSelectors)
        {
            if (rune == null) continue;
            selectedRunes.Add(rune, 0);
        }
    }

    private void Start()
    {

    }

    public static void InitializeStats()
    {
        maxMana = Instance.defaultMana;
        maxStability = Instance.defaultStability;

        maxMana += PlayerInventory.ManaBoost;
        maxStability += PlayerInventory.StabilityBoost;

        foreach (var rune in Instance.runeSelectors)
        {
            rune.maxRunes = rune.defaultRunes + PlayerInventory.GetBonusRunes(rune.Id);
            rune.InitializeRune();
        }
        

        // Initialisation du mana
        currentMana = maxMana;
        interMana = maxMana;
        potentialMana = maxMana;

        Instance.manaSliderUI.maxValue = maxMana;
        Instance.manaSliderUI.value = maxMana;
        Instance.interManaSliderUI.maxValue = maxMana;
        Instance.interManaSliderUI.value = maxMana;
        Instance.potentialManaSliderUI.maxValue = maxMana;
        Instance.potentialManaSliderUI.value = maxMana;

        // Initialisation de la stabilité
        currentStability = maxStability;
        interStability = maxStability;

        Instance.stabilitySliderUI.maxValue = maxStability;
        Instance.stabilitySliderUI.value = maxStability;
        Instance.interStabilitySliderUI.maxValue = maxStability;
        Instance.interStabilitySliderUI.value = maxStability;
        
        // Plaçage des graduations
        // Mana
        for (int i = 0; i < maxMana - 1; i++)
        {
            Instantiate(Instance.manaGrad, Instance.manaGraduationGroup.transform);
        }
        float manaHeight = Instance.manaGraduationGroup.GetComponent<RectTransform>().rect.height;
        float manaSpacing = manaHeight / maxMana;
        manaSpacing -= Instance.manaGrad.GetComponent<RectTransform>().rect.height;

        Instance.manaGraduationGroup.spacing = manaSpacing;

        // Stability
        for (int i = 0; i < maxStability - 1; i++)
        {
            Instantiate(Instance.stabilityGrad, Instance.stabilityGraduationGroup.transform);
        }
        float stabilityHeight = Instance.stabilityGraduationGroup.GetComponent<RectTransform>().rect.width;
        float stabilitySpacing = stabilityHeight / maxStability;
        stabilitySpacing -= Instance.stabilityGrad.GetComponent<RectTransform>().rect.width;

        Instance.stabilityGraduationGroup.spacing = stabilitySpacing;
    }

    private static bool canUsMoreMana(int amount)
    {
        return interMana - amount >= 0;
    }

    private static bool canUsMoreStability(int amount)
    {
        return currentStability - amount >= 0;
    }

    private static void UpdateRuneUI()
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
                Transform slot = Instance.runeHolder.GetChild(runeIndex);

                Instantiate(runeUI, slot);

                runeIndex++;
            }
        }
    }

    private static void DeleteAllRuneUI()
    {
        for (int i = 0; i < Instance.runeHolder.childCount; i++)
        {
            if (Instance.runeHolder.GetChild(i).childCount == 0) continue;

            DestroyImmediate(Instance.runeHolder.GetChild(i).GetChild(0).gameObject);
        }
    }

    // ========================= M�thodes externes ============================= //

    public static bool TryAddRune(Rune rune)
    {   
        if (canUsMoreMana(rune.data.manaCost) && canUsMoreStability(rune.data.manaCost))
        {
            PotentialMana -= rune.data.manaCost;
            InterMana -= rune.data.manaCost;

            CurrentStability -= rune.data.manaCost;
            InterStability -= rune.data.manaCost;

            selectedRunes[rune] += 1;
            UpdateRuneUI();

            SpellCaster.ChangeSpell();
            return true;
        }

        return false;
    }

    public static void RemoveRune(Rune rune, bool restorMana = true)
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

    public static void PreviewRune(RuneData rune)
    {
        PotentialMana -= rune.manaCost;
        InterStability -= rune.manaCost;
    }

    public static void UnPreviewRune(RuneData rune)
    {
        PotentialMana += rune.manaCost;
        InterStability += rune.manaCost;
    }
    
    public static string GetRuneCombinationData(Rune projectedRune = null)
    {   
        string dataPath = $"";
        foreach (Rune rune in Instance.runeSelectors)
        {
            if (rune == null) continue;

            for (int i = 0; i < selectedRunes[rune]; i++)
            {
                dataPath += $"{rune.Id}";
            }

            if (rune == projectedRune)
                dataPath += $"{projectedRune.Id}";
        }

        return dataPath;
    }

    public static void UpdateMana()
    {
        CurrentMana = potentialMana;

        DeleteAllRuneUI();
    }

    public static void ResetSelection(bool restoreRune = false)
    {
        foreach (Rune rune in Instance.runeSelectors)
        {
            if (rune == null) continue;

            if (restoreRune)
            {
                rune.RestoreRune(selectedRunes[rune]);
            }

            RemoveRune(rune, restoreRune);
        }
    }

    public static void ResetMana()
    {
        CurrentMana = maxMana;
        InterMana = maxMana;
        PotentialMana = maxMana;
    }

    public static void ResetStability()
    {
        CurrentStability = maxStability;
        InterStability = maxStability;
    }
}
>>>>>>> origin/gd/Thomas
