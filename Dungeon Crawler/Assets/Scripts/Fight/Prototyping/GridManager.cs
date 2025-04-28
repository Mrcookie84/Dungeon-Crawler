using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] entityList = new GameObject[6];

    public void AddEntity(GameObject entity, Vector2Int gridPos)
    {
        entityList[gridPos.x + 3 * gridPos.y] = entity;
    }

    public GameObject GetEntityAtPos(Vector2Int pos)
    {
        return entityList[pos.x + 3 * pos.y];
    }

    public bool IsPosInGrid(Vector2Int pos)
    {
        return (0 <= pos.x && pos.x <= 2 && 0 <= pos.y && pos.y <= 1);
    }

    public void UpdateEntitiesIndex()
    {
        GameObject[] newEntityList = new GameObject[entityList.Length];
        
        EntityPosition entityPos;
        int entityIndex;
        for (int i = 0; i < entityList.Length; i++)
        {
            if (entityList[i] != null)
            {
                entityPos = entityList[i].GetComponent<EntityPosition>();
                entityIndex = entityPos.gridPos.x + 3 * entityPos.gridPos.y;

                newEntityList[entityIndex] = entityList[i];
            }
        }

        entityList = newEntityList;
    }
}
