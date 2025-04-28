using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EntityPosition gridComponent;
    [SerializeField] private string playerGridTag;
    private GridManager playerGrid;

    private void Start()
    {
        playerGrid = GameObject.FindGameObjectWithTag(playerGridTag).GetComponent<GridManager>();
    }

    public void Attack()
    {
        GameObject target = ChooseTarget();
        if (target != null)
        {
            Debug.Log($"{target.name} attaquée par {gameObject.name} !");
        }
        else
        {
            Debug.Log($"{gameObject.name} rate sont attaque...");
        }
    }

    private GameObject ChooseTarget()
    {
        for (int i = 0; i < playerGrid.entityList.Length / 2; i++)
        {
            if (playerGrid.entityList[i + 3*gridComponent.gridPos.y] != null)
            {
                return playerGrid.entityList[i + 3 * gridComponent.gridPos.y];
            }
        }

        return null;
    }
}
