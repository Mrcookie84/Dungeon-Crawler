using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Rune : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Rune Infos")]
    public RuneData data;
    [SerializeField] private Button runeButton;
    public int maxSelected;
    public int defaultRunes;
    [HideInInspector] public int maxRunes;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI linkedText;
    [Space(5)]
    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private Image cooldownFill;
    [SerializeField] private Sprite cooldownNormalSprite;
    [SerializeField] private Sprite cooldownDepletedSprite;
    [Space(5)]
    [SerializeField] private Image overlayImage;
    [SerializeField] private Sprite highlightSprite;
    [SerializeField] private Sprite highlightUnstableSprite;
    [SerializeField] private Sprite blockedSprite;

    private SortedDictionary<int, int> _coolDownPool = new SortedDictionary<int, int>();
    private bool previewed;

    public int Id { get { return data.ID; } }
    public int CoolDown { get { return data.cooldown; } }

    public int MinCooldown
    {
        get
        {
            for (int i = 1; i < CoolDown + 1; i++)
            {
                if (_coolDownPool[i] > 0)
                {
                    return i;
                }
            }

            return 0;
        }
    }

    private void Awake()
    {
        InitializeRune();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void InitializeRune()
    {
        _coolDownPool = new SortedDictionary<int, int>();

        for (int i = 0; i < CoolDown + 1; i++)
        {
            _coolDownPool.Add(i, 0);
        }
        _coolDownPool[0] = maxRunes;

        if (cooldownSlider != null)
            cooldownSlider.maxValue = data.cooldown;
    }

    public void UpdateRuneSelection()
    {
        if (_coolDownPool[0] <= 0) return;

        if (RuneSelection.selectedRunes[this] < maxSelected)
        {
            if (RuneSelection.TryAddRune(this))
            {
                _coolDownPool[0]--;
                _coolDownPool[CoolDown]++;

                UpdateUI();

                return;
            }
            return;
        }

        RuneSelection.RemoveRune(this);
        _coolDownPool[0] += maxSelected;
        _coolDownPool[CoolDown] -= maxSelected;

        UpdateUI();
    }

    public void UpdateCooldown()
    {
        _coolDownPool[0] += _coolDownPool[1];

        for (int i = 1; i < CoolDown; i++)
        {
            _coolDownPool[i] = _coolDownPool[i+1];
        }

        _coolDownPool[CoolDown] = 0;

        UpdateUI();
    }

    public void RestoreRune(int amount)
    {
        _coolDownPool[0] += amount;
        _coolDownPool[CoolDown] -= amount;

        UpdateUI();
    }

    public void UpdateUI()
    {
        // Mise � jour text
        linkedText.text = _coolDownPool[0].ToString();

        // Mise à jour de l'état
        Debug.Log($"{data.name} =====================");

        bool validSpell = false;
        bool unstableSpell = false;
        string spellString = "";
        switch (SpellCaster.currentCastMode)
        {
            // Sort sur les personnages
            case SpellCaster.CastMode.Player:
                spellString = "Spells/SpellsPlayer/SpellPlayerData" + RuneSelection.GetRuneCombinationData(this);
                Debug.Log(spellString);

                SpellPlayerData potentialPSpell = Resources.Load<SpellPlayerData>(spellString);

                if (potentialPSpell != null)
                    validSpell = true;

                Debug.Log(validSpell);
                break;
            
            // Sort sur les ennemis
            case SpellCaster.CastMode.Enemy:
                spellString = "Spells/SpellsEnemy/SpellEnemyData" + RuneSelection.GetRuneCombinationData(this);
                Debug.Log(spellString);

                SpellEnemyData potentialESpell = Resources.Load<SpellEnemyData>(spellString);

                if (potentialESpell != null)
                {
                    validSpell = true;

                    if (potentialESpell.unstable)
                        unstableSpell = true;
                }

                Debug.Log(validSpell);
                break;
        }
        
        // Changer l'overlay
        if (overlayImage != null)
        {
            runeButton.interactable = true;

            if (RuneSelection.IsEmpty && Id <= 2)
            {
                Debug.Log("First Rune");

                overlayImage.sprite = null;
                overlayImage.gameObject.SetActive(false);
            }
            else
            {
                overlayImage.gameObject.SetActive(true);

                if (validSpell)
                {
                    Debug.Log("Valide !");

                    if (unstableSpell)
                        overlayImage.sprite = highlightUnstableSprite;
                    else
                        overlayImage.sprite = highlightSprite;
                }
                else
                {
                    Debug.Log("In-valide ...");

                    runeButton.interactable = false;
                    overlayImage.sprite = blockedSprite;
                }
            }
        }
        
        // Mise � jour du cooldown
        if (cooldownSlider == null) return;

        cooldownSlider.maxValue = CoolDown;
        cooldownSlider.value = MinCooldown;

        if (_coolDownPool[0] == 0)
            cooldownFill.sprite = cooldownDepletedSprite;
        else
            cooldownFill.sprite = cooldownNormalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!previewed && runeButton.interactable)
        {
            RuneSelection.PreviewRune(data);
            previewed = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (previewed && runeButton.interactable)
        {
            RuneSelection.UnPreviewRune(data);
            previewed = false;
        }
    }
}
