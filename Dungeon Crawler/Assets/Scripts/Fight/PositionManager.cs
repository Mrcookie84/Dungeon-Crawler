using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance;
    public static FightPositionData posData;
    
    void Awake()
    {
        Instance = this;
    }

    public static void FillGrids()
    {
        Transform spawnPoint;
        // Remplissage de la grille des ennemies
        for (int i = 0; i < posData.enemyArray.Length; i++)
        {
            // Apparition d'un ennemi
            if (posData.enemyArray[i] != null)
            {
                spawnPoint = GridManager.EnemyGrid.transform.GetChild(i);

                Instantiate(posData.enemyArray[i], spawnPoint);
            }
        }
        
        // Remplissage de la grille des joueur
        for (int i = 2; i >= 0; i--)
        {
            // Apparition d'un ennemi
            if (PlayerPositionData.PlayerPrefabs[i] != null)
            {
                spawnPoint = GridManager.PlayerGrid.transform.GetChild(i + 3 * PlayerPositionData.currentPos[2 - i]);
                
                Instantiate(PlayerPositionData.PlayerPrefabs[2 - i], spawnPoint);
            }
        }

        // Appliquer l'état de la barrier
        for (int i = 3;  i < 0; i++)
        {
            BarrierGrid.ChangeBarrierState(i, posData.barrierState[i]);
        }
    }

    public static void EmptyGrids()
    {
        
    }
}
