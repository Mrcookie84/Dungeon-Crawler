using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rune : MonoBehaviour
{
    [Header("Rune Infos")]
    public RuneData data;
    [SerializeField] private Button runeButton;
    [SerializeField] private string runeSelectionTag = "SpellManager";
    private RuneSelection runeSelection;
    private SpellCaster spellCaster;
    public int maxSelected;
    public int maxRunes;

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

    public int Id { get { return data.ID; } }
    public int CoolDown { get { return data.cooldown; } }

    public int MinCooldown
    {
        get
        {
            foreach (int nbTurn in _coolDownPool.Keys)
            {
                if (_coolDownPool[nbTurn] > 0)
                {
                    return nbTurn;
                }
            }

            return _coolDownPool.Count - 1;
        }
    }

    private void Start()
    {
        runeSelection = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<RuneSelection>();
        spellCaster = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<SpellCaster>();

        for (int i = 0; i < CoolDown + 1; i++)
        {
            _coolDownPool.Add(i, 0);
        }
        _coolDownPool[0] = maxRunes;

        if (cooldownSlider != null)
            cooldownSlider.maxValue = data.cooldown;

        UpdateUI();
    }

    public void UpdateRuneSelection()
    {
        if (_coolDownPool[0] <= 0) return;

        if (runeSelection.selectedRunes[this] < maxSelected)
        {
            if (runeSelection.TryAddRune(this))
            {
                _coolDownPool[0]--;
                _coolDownPool[CoolDown]++;

                UpdateUI();

                return;
            }
            return;
        }

        runeSelection.RemoveRune(this);
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
        bool validSpell = false;
        bool unstableSpell = false;
        string spellString = "";
        switch (spellCaster.currentCastMode)
        {
            // Sort sur les personnages
            case SpellCaster.CastMode.Player:
                spellString = runeSelection.CurrentSpellPlayer + Id;
                
                SpellPlayerData potentialPSpell = Resources.Load<SpellPlayerData>(spellString);

                if (potentialPSpell != null)
                    validSpell = true;
                
                break;
            
            // Sort sur les ennemis
            case SpellCaster.CastMode.Enemy:
                spellString = runeSelection.CurrentSpellEnemy + Id;
                SpellEnemyData potentialESpell = Resources.Load<SpellEnemyData>(spellString);

                if (potentialESpell != null)
                {
                    validSpell = true;

                    if (potentialESpell.unstable)
                        unstableSpell = true;
                }
                
                break;
        }
        
        // Changer l'overlay
        if (overlayImage != null)
        {
            runeButton.interactable = true;

            if (runeSelection.IsEmpty)
            {
                overlayImage.sprite = null;
                overlayImage.gameObject.SetActive(false);
            }
            else
            {
                overlayImage.gameObject.SetActive(true);

                if (validSpell)
                {
                    if (unstableSpell)
                        overlayImage.sprite = highlightUnstableSprite;
                    else
                        overlayImage.sprite = highlightSprite;
                }
                else
                {
                    runeButton.interactable = false;
                    overlayImage.sprite = blockedSprite;
                }
            }
        }
        
        // Mise � jour du cooldown
        if (cooldownSlider == null) return;

        cooldownSlider.value = MinCooldown;

        if (_coolDownPool[0] == 0)
            cooldownFill.sprite = cooldownDepletedSprite;
        else
            cooldownFill.sprite = cooldownNormalSprite;
    }
}
