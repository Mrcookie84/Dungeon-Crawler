using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridManager : MonoBehaviour
{
    public GameObject[] entityList = new GameObject[6];
    
    public UnityEvent gridUpdate = new UnityEvent();

    public void AddEntity(GameObject entity, Vector2Int gridPos)
    {
        entityList[gridPos.x + 3 * gridPos.y] = entity;

        gridUpdate.Invoke();
    }

    public GameObject GetEntityAtPos(Vector2Int pos)
    {
        return entityList[pos.x + 3 * pos.y];
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
            //Debug.Log($"Case iluminée : ({cell.x}, {cell.y})");
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
}
