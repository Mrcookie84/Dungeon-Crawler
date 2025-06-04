using UnityEngine;
using UnityEngine.Events;

public class EntityPosition : MonoBehaviour
{
    [SerializeField] private string gridTag;
    private Transform grid; 
    public Vector2Int gridPos;

    public UnityEvent isMoving = new UnityEvent();

    public int gridIndex
    {
        get
        {
            return gridPos.x + 3 * gridPos.y;
        }
    }

    public GridManager LinkedGrid
    {
        get { return grid.GetComponent<GridManager>(); }
    }

    private void Start()
    {
        grid = GameObject.FindGameObjectWithTag(gridTag).transform;
        GridManager gridManager = grid.GetComponent<GridManager>();
        
        FindGridPos();
        gridManager.AddEntity(gameObject, gridPos);
    }

    public void ChangePosition(Vector2Int newPos)
    {
        Debug.Log($"{gameObject.name} : {gridPos} → {newPos}");
        
        gridPos = newPos;
        transform.parent = grid.GetChild(newPos.x + 3 * newPos.y);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        isMoving.Invoke();
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
