using UnityEngine;

[CreateAssetMenu(fileName = "RuneData", menuName = "ScriptableObject/Spells/RuneData")]
public class RuneData : ScriptableObject
{
    public int ID;
    public int manaCost;
    public int cooldown;
    public GameObject UIPrefab;
}
