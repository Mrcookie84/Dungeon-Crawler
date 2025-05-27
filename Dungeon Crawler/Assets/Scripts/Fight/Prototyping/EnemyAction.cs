using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    [Header("Position infos")]
    [SerializeField] protected EntityPosition gridComp;
    [SerializeField] protected string playerGridTag;
    protected GridManager playerGrid;
    [SerializeField] protected string enemyGridTag;
    protected GridManager enemyGrid;
    [SerializeField] protected string barrierGridTag;
    protected BarrierGrid barrierGrid;

    [Header("Actions")]
    [SerializeField] private List<EnemyActionData> actions = new List<EnemyActionData>();
    [SerializeField] private List<int> probActions = new List<int>();
    private Dictionary<EnemyActionData, int> actionTable = new Dictionary<EnemyActionData, int>();

    [Header("Animator")]
    [SerializeField] protected EntityFightAnimation animHandler;
    
    private void Start()
    {
        playerGrid = GameObject.FindGameObjectWithTag(playerGridTag).GetComponent<GridManager>();
        enemyGrid = GameObject.FindGameObjectWithTag(enemyGridTag).GetComponent<GridManager>();
        barrierGrid = GameObject.FindGameObjectWithTag(barrierGridTag).GetComponent<BarrierGrid>();

        actionTable = new Dictionary<EnemyActionData, int>();

        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i] == null) continue;
            actionTable.Add(actions[i], probActions[i]);
        }
    }
    
    public virtual void DoAction()
    {
        int rng = Random.Range(0, 100);
        int prob = 0;

        foreach (EnemyActionData action in actionTable.Keys)
        {
            prob += actionTable[action];
            if (rng < prob)
            {
                Debug.Log(action.name);

                if (action.isAttack)
                {
                    Attack(action);
                }
                else
                {
                    Support(action);
                }
                
                break;
            }
        }
    }

    protected virtual void Attack(EnemyActionData action)
    {
        animHandler.ChangeState(EntityFightAnimation.State.Attack);

        List<GameObject> targets = new List<GameObject>();

        if (action.multipleTarget)
        {
            targets = playerGrid.GetEntitiesOnRow(gridComp.gridPos.y);
        }
        else
        {
            targets.Add(playerGrid.GetRandomEntityOnRow(gridComp.gridPos.y));
        }

        foreach (GameObject entity in targets)
        {
            if (entity == null) continue;

            if (action.dmgValue > 0)
            {
                EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
                entityHealth.TakeDamage(gameObject, action.dmgValue);
            }

            if (action.status != null)
            {
                EntityStatusHolder entityStatus = entity.GetComponent<EntityStatusHolder>();
                entityStatus.AddStatus(action.status, action.statusDuration, gameObject);
            }
        }
    }

    protected virtual void Support(EnemyActionData action)
    {
        List<GameObject> targets = new List<GameObject>();

        switch (action.supportTarget)
        {
            case EnemyActionData.EnemySupportTarget.Self:
                targets.Add(gameObject);
                break;

            case EnemyActionData.EnemySupportTarget.Other:
                break;

            case EnemyActionData.EnemySupportTarget.SelfOrOther:
                break;

            case EnemyActionData.EnemySupportTarget.Multiple:
                targets = enemyGrid.GetAdjacentEntities(gridComp.gridPos);
                break;
        }

        foreach (GameObject entity in targets)
        {
            if (action.healAmount > 0)
            {
                EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
                entityHealth.Heal(action.dmgValue);
            }

            if (action.status != null)
            {
                EntityStatusHolder entityStatus = entity.GetComponent<EntityStatusHolder>();
                entityStatus.AddStatus(action.status, action.statusDuration, gameObject);
            }
        }
    }
}
