using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    [Header("Position infos")]
    [SerializeField] protected EntityPosition gridComp;
    [SerializeField] protected string playerGridTag;
    protected GridManager playerGrid;
    [SerializeField] protected string enemyGridTag;
    protected GridManager enemyGrid;
    [SerializeField] protected string barrierGridTag;
    protected BarrierGrid barrierGrid;
    
    private void Start()
    {
        playerGrid = GameObject.FindGameObjectWithTag(playerGridTag).GetComponent<GridManager>();
        enemyGrid = GameObject.FindGameObjectWithTag(enemyGridTag).GetComponent<GridManager>();
        barrierGrid = GameObject.FindGameObjectWithTag(barrierGridTag).GetComponent<BarrierGrid>();
    }
    
    public abstract void DoAction();
}
