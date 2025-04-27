using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Equipement", menuName = "ScriptableObject/Equipement")]
public class EquipementData : ScriptableObject 
{
    public Sprite imageEquipment;
    [SerializeField]
    private List<Runes> listOfRunes = new List<Runes>();
}
