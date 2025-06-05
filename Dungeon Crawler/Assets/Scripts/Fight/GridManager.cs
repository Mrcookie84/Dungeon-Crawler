using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager PlayerGrid;
    public static GridManager EnemyGrid;
    
    public GameObject[] entityList = new GameObject[6];
    public CellHighlighter[] highlighterList = new CellHighlighter[6];
    
    public UnityEvent gridUpdate = new UnityEvent();

    public bool IsEmpty
    {
        get
        {
            bool result = true;
            foreach (var entity in entityList)
            {
                if (entity != null)
                {
                    EntityHealth health = entity.GetComponent<EntityHealth>();

                    if (!health.dead)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }

    public static bool IsPlayerGridEmpty
    {
        get { return PlayerGrid.IsEmpty; }
    }

    public static bool IsEnemyGridEmpty
    {
        get { return EnemyGrid.IsEmpty; }
    }

    private void Awake()
    {
        if (tag == "EnemyGrid")
            EnemyGrid = this;
        else if (tag == "PlayerGrid")
            PlayerGrid = this;
    }

    public void AddEntity(GameObject entity, Vector2Int gridPos)
    {
        entityList[gridPos.x + 3 * gridPos.y] = entity;

        gridUpdate.Invoke();
    }

    public bool IsPosInGrid(Vector2Int pos)
    {
        bool xCheck = 0 <= pos.x && pos.x <= 2;
        bool yCheck = 0 <= pos.y && pos.y <= 1;

        return (xCheck && yCheck);
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

        gridUpdate.Invoke();
    }

    public void EmptyGrid()
    {
        entityList = new GameObject[6];

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount < 2) continue;
            
            Destroy(transform.GetChild(i));
        }
    }

    public void HighlightCells(List<CellHighlighter.HighlightInfo> cellList)
    {
        foreach (var hlInfo in cellList)
        {
            highlighterList[hlInfo.cell.x + 3 * hlInfo.cell.y].Highlight(hlInfo);
        }
    }

    public void ResetHighlight()
    {
        for (int i = 0; i < 6; i++)
        {
            highlighterList[i].ResetHL();
        }
    }


    // Détection des entités
    public GameObject GetEntityAtPos(Vector2Int pos)
    {
        if (!IsPosInGrid(pos))
        {
            return null;
        }

        return entityList[pos.x + 3 * pos.y];
    }

    public GameObject[] GetEntitiesAtMultPos(List<Vector2Int> posArray)
    {
        GameObject[] entityArray = new GameObject[posArray.Count];

        int i = 0;
        foreach (Vector2Int pos in posArray)
        {
            entityArray[i] = GetEntityAtPos(pos);
            i++;
        }

        return entityArray;
    }

    public List<GameObject> GetEntitiesOnRow(int row, GameObject excludedEntity = null)
    {
        List<GameObject> rowList = new List<GameObject>();
        for (int i = 0;i < 3; i++)
        {
            if (entityList[i + row*3] != null && entityList[i + row * 3] != excludedEntity)
            {
                rowList.Add(entityList[i + row * 3]);
            }
        }

        return rowList;
    }

    public GameObject GetRandomEntityOnRow(int row)
    {
        List<GameObject> possibleEntities = GetEntitiesOnRow(row);
        int rng = Random.Range(0, possibleEntities.Count);

        if (possibleEntities.Count != 0)
            return possibleEntities[rng];
        else
            return null;
    }

    public List<GameObject> GetAdjacentEntities(Vector2Int cellCoords, GameObject excludedEntity = null)
    {
        List<GameObject> adjacentList = new List<GameObject>();

        Vector2Int[] adjacentPoses = new Vector2Int[4];
        adjacentPoses[0] = new Vector2Int(0, 1);
        adjacentPoses[1] = new Vector2Int(1, 0);
        adjacentPoses[2] = new Vector2Int(0, -1);
        adjacentPoses[3] = new Vector2Int(-1, 0);

        foreach (Vector2Int adjacentCell in adjacentPoses)
        {
            Vector2Int currentCell = cellCoords + adjacentCell;
            if (!IsPosInGrid(currentCell)) continue;

            GameObject currentEntity = GetEntityAtPos(currentCell);

            if (currentEntity != null && currentEntity != excludedEntity)
            {
                adjacentList.Add(currentEntity);
            }
        }

        return adjacentList;
    }
}
