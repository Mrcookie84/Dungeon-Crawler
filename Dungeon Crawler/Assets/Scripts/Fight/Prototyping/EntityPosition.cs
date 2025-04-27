using System;
using UnityEngine;

public class EntityPosition : MonoBehaviour
{
    [SerializeField] private string gridTag;
    private Transform grid; 
    public Vector2Int gridPos;

    private void Start()
    {
        grid = GameObject.FindGameObjectWithTag(gridTag).transform;
        GridManager gridManager = grid.GetComponent<GridManager>();
        
        FindGridPos();
        gridManager.AddEntity(gameObject, gridPos);
    }

    public void ChangePosition(Vector2Int newPos)
    {
        gridPos = newPos;
        
        // Retourner l'entité si elle es en bas
        if (newPos.y == -1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void FindGridPos()
    {
        for (int i = 0; i < 6; i++)
        {
            if (transform.position == grid.GetChild(i).transform.position)
            {
                gridPos = new Vector2Int(i % 3, i / 3);
            }
        }
    }
}
