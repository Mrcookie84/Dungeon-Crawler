using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Rune : MonoBehaviour
{
    [Header("Rune Infos")]
    public RuneData data;
    [SerializeField] private string runeSelectionTag = "SpellManager";
    private RuneSelection runeSelection;
    public int maxSelected;
    public int maxRunes;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI linkedText;
    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private Image cooldownFill;
    [SerializeField] private Sprite cooldownNormalSprite;
    [SerializeField] private Sprite cooldownDepletedSprite;

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
        // Mise à jour text
        linkedText.text = _coolDownPool[0].ToString();

        // Mise à jour du cooldown
        if (cooldownSlider == null) return;

        cooldownSlider.value = MinCooldown;

        if (_coolDownPool[0] == 0)
            cooldownFill.sprite = cooldownDepletedSprite;
        else
            cooldownFill.sprite = cooldownNormalSprite;
    }
}
