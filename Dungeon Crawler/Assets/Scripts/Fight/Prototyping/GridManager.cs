using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridManager : MonoBehaviour
{
    public GameObject[] entityList = new GameObject[6];
    
    public UnityEvent gridUpdate = new UnityEvent();

    public bool isEmpty
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

    public void HighlightCells(List<Vector2Int> cellList)
    {
        foreach (Vector2Int cell in cellList)
        {
            //Debug.Log($"Case ilumin�e : ({cell.x}, {cell.y})");
            transform.GetChild(cell.x + cell.y * 3).GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ResetHighlight()
    {
        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }


    // D�tection des entit�s
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
