using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    [SerializeField] private TMPro.TextMeshProUGUI linkedText;
    [SerializeField] private string runeSelectionTag = "SpellManager";
    private RuneSelection runeSelection;
    public int maxSelected;
    public int maxRunes;

    private SortedDictionary<int, int> _coolDownPool = new SortedDictionary<int, int>();

    public int Id { get { return data.ID; } }
    public int CoolDown { get { return data.cooldown; } }

    private void Start()
    {
        runeSelection = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<RuneSelection>();

        for (int i = 0; i < CoolDown + 1; i++)
        {
            _coolDownPool.Add(i, 0);
        }
        _coolDownPool[0] = maxRunes;

        linkedText.text = _coolDownPool[0].ToString();
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

                linkedText.text = _coolDownPool[0].ToString();

                return;
            }
            return;
        }

        runeSelection.RemoveRune(this);
        _coolDownPool[0] += maxSelected;
        _coolDownPool[CoolDown] -= maxSelected;

        linkedText.text = _coolDownPool[0].ToString();
    }

    public void UpdateCooldown()
    {
        /*
        string debug = $"{name} - ";
        foreach (int count in _coolDownPool.Keys)
        {
            debug += $"{count} : {_coolDownPool[count]} || ";
        }
        Debug.Log(debug);
        */
        
        _coolDownPool[0] += _coolDownPool[1];

        for (int i = 1; i < CoolDown; i++)
        {
            _coolDownPool[i] = _coolDownPool[i+1];
        }

        _coolDownPool[CoolDown] = 0;

        linkedText.text = _coolDownPool[0].ToString();
    }

    public void RestoreRune(int amount)
    {
        _coolDownPool[0] += amount;
        _coolDownPool[CoolDown] -= amount;

        linkedText.text = _coolDownPool[0].ToString();
    }
}
