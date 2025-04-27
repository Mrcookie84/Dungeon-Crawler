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

    //public bool IsPosInGrid(Vector2Int pos){  }
}
