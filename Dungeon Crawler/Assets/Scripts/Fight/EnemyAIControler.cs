using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAIControler : MonoBehaviour
{
    public static EnemyAIControler Instance;
    public static bool[] playerGridMask = new bool[6];
    public static bool[] enemyGridMask = new bool[6];
    public static bool[] barrierGridMask = new bool[3];
    
    // ======================= Méthodes ======================== //
    // Static
    public static void UpdateAllMasks()
    {
        UpdateBarrierMask();
        UpdateEnemyMask();
        UpdatePlayerMask();
    }
    
    public static void UpdatePlayerMask()
    {
        GridManager.PlayerGrid.UpdateEntitiesIndex();

        for (int i = 0; i < playerGridMask.Length; i++)
        {
            GameObject entity = GridManager.PlayerGrid.GetEntityAtPos(new Vector2Int(i % 3, i / 3));

            if (entity != null)
                playerGridMask[i] = true;
            else
                playerGridMask[i] = false;
            
            //Debug.Log(playerGridMask[i]);
        }
    }

    public static void UpdateEnemyMask()
    {
        GridManager.EnemyGrid.UpdateEntitiesIndex();

        for (int i = 0; i < enemyGridMask.Length; i++)
        {
            GameObject entity = GridManager.EnemyGrid.GetEntityAtPos(new Vector2Int(i % 3, i / 3));

            if (entity != null)
                enemyGridMask[i] = true;
            else
                enemyGridMask[i] = false;
        }
    }

    public static void UpdateBarrierMask()
    {
        for (int i = 0; i < barrierGridMask.Length; i++)
        {
            if (BarrierGrid.IsBarrierBroken(i))
                barrierGridMask[i] = false;
            else
                barrierGridMask[i] = true;
        }
    }

    public static int CountPlayerOnRow(int row)
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            if (playerGridMask[row * 3 + i]) count ++;
        }

        return count;
    }

    public static int CountEnemyOnRow(int row)
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            if (enemyGridMask[row * 3 + i]) count++;
        }

        return count;
    }

    public static bool IsPlayerOnCell(Vector2Int cell)
    {
        return playerGridMask[cell.x + 3 * cell.y];
    }

    public static bool IsEnemyOnCell(Vector2Int cell)
    {
        return enemyGridMask[cell.x + 3 * cell.y];
    }

    public static GameObject GetPlayerOnCell(Vector2Int cell)
    {
        return GridManager.PlayerGrid.entityList[cell.x + 3 * cell.y];
    }

    public static GameObject GetEnemyOnCell(Vector2Int cell)
    {
        return GridManager.EnemyGrid.entityList[cell.x + 3 * cell.y];
    }

    public static bool IsBarrierActive(Vector2Int cell)
    {
        return barrierGridMask[cell.x];
    }

    // Non - static
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateAllMasks();
    }
}
