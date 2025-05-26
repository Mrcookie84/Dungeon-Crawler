using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Equipement", menuName = "ScriptableObject/Equipement")]
    public class Item : ScriptableObject
    {

        public ItemData ItemData;

    }
}