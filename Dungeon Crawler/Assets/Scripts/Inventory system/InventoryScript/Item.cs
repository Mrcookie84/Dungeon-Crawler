using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Equipement", menuName = "ScriptableObject/Equipement")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite sprite;
        public int spaceDmgBonus;
        public int realityDmgBonus;
        
        public enum ItemSlot
        {
            Weapon,
            Armor,
            Accessories
        }

        public ItemSlot itemSlot;

    }
}