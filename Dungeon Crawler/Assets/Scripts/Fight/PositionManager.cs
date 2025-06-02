using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance;
    public static FightPositionData posData;
    
    [SerializeField] private Transform playerGrid;
    [SerializeField] private Transform enemyGrid;

    private static Transform PlayerGrid
    {
        get { return Instance.playerGrid; }
    }

    private static Transform EnemyGrid
    {
        get { return Instance.enemyGrid; }
    }
    
    
    
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
                spawnPoint = EnemyGrid.GetChild(i);

                Instantiate(posData.enemyArray[i], spawnPoint);
            }
        }
        
        // Remplissage de la grille des joueur
        for (int i = 0; i < posData.playerArray.Length; i++)
        {
            // Apparition d'un ennemi
            if (posData.playerArray[i] != null)
            {
                spawnPoint = PlayerGrid.GetChild(i);
                
                Instantiate(posData.playerArray[i], spawnPoint);
            }
        }
    }

    public static void EmptyGrids()
    {
        
    }
}
