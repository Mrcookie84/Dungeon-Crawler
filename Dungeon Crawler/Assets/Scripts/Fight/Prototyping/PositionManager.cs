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
                
                // Orientation selon le monde d'apparition
                // i <= 3 : case du haut
                // i > 3  : case du bas
                if (i <= 3)
                {
                    Instantiate(posData.enemyArray[i], spawnPos, Quaternion.Euler(new Vector3(0f, 180f, 0f)));
                }
                else
                {
                    Instantiate(posData.enemyArray[i], spawnPos, Quaternion.Euler(new Vector3(180f, 180f, 0f)));
                }
            }
        }
        
        // Remplissage de la grille des joueur
        for (int i = 0; i < posData.playerArray.Length; i++)
        {
            // Apparition d'un ennemi
            if (posData.playerArray[i] != null)
            {
                spawnPos = playerGrid.GetChild(i).position;
                
                // Orientation selon le monde d'apparition
                // i <= 3 : case du haut
                // i > 3  : case du bas
                if (i <= 3)
                {
                    Instantiate(posData.playerArray[i], spawnPos, Quaternion.Euler(new Vector3(0f, 0f, 0)));
                    
                }
                else
                {
                    Instantiate(posData.playerArray[i], spawnPos, Quaternion.Euler(new Vector3(180f, 0f, 0f)));
                    // Je sais que l'ennemi regarde vers la droite mais ¯\_o_/¯    - Alexandre
                }
            }
        }
    }
    
    void Update()
    {
        
    }
}
