using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] private FightPositionData posData;
    [SerializeField] private Transform playerGrid;
    [SerializeField] private Transform enemyGrid;
    
    void Awake()
    {
        
        Vector3 spawnPos;
        // Remplissage de la grille des ennemies
        for (int i = 0; i < posData.enemyArray.Length; i++)
        {
            // Apparition d'un ennemi
            if (posData.enemyArray[i] != null)
            {
                spawnPos = enemyGrid.GetChild(i).position;

                Instantiate(posData.enemyArray[i], spawnPos, Quaternion.Euler(Vector3.zero), transform.parent);
            }
        }
        
        // Remplissage de la grille des joueur
        for (int i = 0; i < posData.playerArray.Length; i++)
        {
            // Apparition d'un ennemi
            if (posData.playerArray[i] != null)
            {
                spawnPos = playerGrid.GetChild(i).position;
                
                Instantiate(posData.playerArray[i], spawnPos, Quaternion.Euler(Vector3.zero), transform.parent);
            }
        }
    }
}
