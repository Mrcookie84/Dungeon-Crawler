using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    [SerializeField] private string runeSelectionTag;
    private RuneSelection runeSelection;
    public int maxSelected;

    private void Start()
    {
        runeSelection = GameObject.FindGameObjectWithTag(runeSelectionTag).GetComponent<RuneSelection>();
    }

    public void UpdateRuneSelection()
    {
        if (runeSelection.selectedRunes[this] < maxSelected)
        {
            runeSelection.TryAddRune(this);
        }
        else
        {
            runeSelection.RemoveRune(this);
        }
    }
    
    public int GetID()
    {
        return data.ID;
    }
}
