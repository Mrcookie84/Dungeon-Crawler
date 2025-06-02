using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIControler : MonoBehaviour
{
    public static EnemyAIControler Instance;
    public static bool[] playerGridMask = new bool[6];
    public static bool[] enemyGridMask = new bool[6];
    public static bool[] barrierGridMask = new bool[3];

    public GridManager playerGrid;
    public GridManager enemyGrid;
    public BarrierGrid barrierGrid;

    // ====================== Propriété ======================== //
    public static GridManager PlayerGrid
    {
        get { return Instance.playerGrid; }
    }

    public static GridManager EnemyGrid
    {
        get { return Instance.enemyGrid; }
    }

    public static BarrierGrid BarrierGrid
    {
        get { return Instance.barrierGrid; }
    }

    // ======================= Méthodes ======================== //
    // Static
    public static void UpdatePlayerMask()
    {
        PlayerGrid.UpdateEntitiesIndex();

        for (int i = 0; i < playerGridMask.Length; i++)
        {
            GameObject entity = PlayerGrid.GetEntityAtPos(new Vector2Int(i / 3, i % 3));

            if (entity != null)
                playerGridMask[i] = true;
            else
                playerGridMask[i] = false;
        }
    }

    public static void UpdateEnemyMask()
    {
        EnemyGrid.UpdateEntitiesIndex();

        for (int i = 0; i < enemyGridMask.Length; i++)
        {
            GameObject entity = EnemyGrid.GetEntityAtPos(new Vector2Int(i / 3, i % 3));

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
        return PlayerGrid.entityList[cell.x + 3 * cell.y];
    }

    public static GameObject GetEnemyOnCell(Vector2Int cell)
    {
        return EnemyGrid.entityList[cell.x + 3 * cell.y];
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
        UpdateBarrierMask();
    }
}
