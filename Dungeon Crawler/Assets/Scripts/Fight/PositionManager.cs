using Unity.VisualScripting;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance;
    public static FightPositionData posData;

    public static PlayerPositionData playerPos;
    
    void Awake()
    {
        Instance = this;
        
        playerPos = Resources.Load<PlayerPositionData>("FightPositions/PlayerPosition");
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
            if (playerPos.playerPrefabs[i] != null)
            {
                spawnPoint = GridManager.PlayerGrid.transform.GetChild(i + 3 * playerPos.currentPos[i]);
                
                Instantiate(playerPos.playerPrefabs[2 - i], spawnPoint);
            }
        }

        // Appliquer l'�tat de la barrier
        for (int i = 3;  i < 0; i++)
        {
            BarrierGrid.ChangeBarrierState(i, posData.barrierState[i]);
        }
    }

    public static void EmptyGrids()
    {
        for (int i = 0; i < 6; i++)
        {
            GridManager.EnemyGrid.entityList = new GameObject[6];
            if (GridManager.EnemyGrid.transform.GetChild(i).childCount == 2)
                Destroy(GridManager.EnemyGrid.transform.GetChild(i).GetChild(1));
            
            GridManager.PlayerGrid.entityList = new GameObject[6];
            if (GridManager.PlayerGrid.transform.GetChild(i).childCount == 2)
                Destroy(GridManager.PlayerGrid.transform.GetChild(i).GetChild(1));
        }
    }
}
