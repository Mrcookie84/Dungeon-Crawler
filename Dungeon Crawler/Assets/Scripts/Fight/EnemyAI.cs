using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Components reference")]
    [SerializeField] private EntityPosition gridComp;
    [SerializeField] private EntityFightAnimation animHandler;

    [Header("Actions")]
    [SerializeField] private List<EnemyActionData> actions;
    [SerializeField] private bool canBreakBarrier;

    [Header("Position weights")]
    [SerializeField] private int[] columnW = new int[3];
    [Space(5)]
    [SerializeField] private int dimAffinityRow = 0;
    [SerializeField] private int dimAffintyW = 0;

    [Header("Entities weights")]
    //[SerializeField] private int adjacentEnemyW = 0;
    [SerializeField] private int enemyOnCellW = 0;
    [Space(5)]
    [SerializeField] private int enemyInWorldW = 0;
    [SerializeField] private int playerInWorldW = 0;
    [Space(5)]
    //[SerializeField] private int weakestPlayerRowW = 0;
    //[SerializeField] private int weakestEnemyAdjW = 0;

    [Header("Choice bias")]
    [SerializeField] private int nbTurnForesight;
    [SerializeField] private int initialScoreDif;
    [SerializeField] private int scoreDifDecay;
    private int scoreDif;
    

    private int[] scoreMask = new int[6];
    private Dictionary<Vector2Int, List<EnemyActionData>> sortedActions = new Dictionary<Vector2Int, List<EnemyActionData>>();

    // ====================== Propriétés ======================== //
    public Vector2Int[] Movements
    {
        get
        {
            return sortedActions.Keys.ToArray();
        }
    }

    public Vector2Int GridPos
    {
        get { return gridComp.gridPos; }
        set { gridComp.gridPos = value; }
    }

    // ======================= Méthodes ========================= //
    // Static
    private static int ActionWeightsSum(List<EnemyActionData> actionList)
    {
        int sum = 0;
        foreach (EnemyActionData actionData in actionList)
            sum += actionData.weight;

        return sum;
    }

    public static GameObject RandomEntityInList(List<GameObject> entityList)
    {
        int rng = UnityEngine.Random.Range(0, entityList.Count);

        return entityList[rng];
    }

    // Public
    public void UpdateMask()
    {
        for (int i = 0; i < scoreMask.Length; i++)
        {
            Vector2Int cell = new Vector2Int(i % 3, i / 3);
            scoreMask[i] = 0;

            // Position weights
            if (cell.y == dimAffinityRow)
            {
                scoreMask[i] += dimAffintyW;
            }

            scoreMask[i] += columnW[cell.x];

            // Entity weights
            scoreMask[i] += EnemyAIControler.IsEnemyOnCell(cell) && i != GridPos.x+3*GridPos.y ? enemyOnCellW : 0;
            scoreMask[i] += EnemyAIControler.CountEnemyOnRow(cell.y) * enemyInWorldW;
            scoreMask[i] += EnemyAIControler.CountPlayerOnRow(cell.y) * playerInWorldW;
        }
    }

    public void ResetMoveInitiative()
    {
        scoreDif = initialScoreDif;
    }

    public void UpdateMoveInitiative()
    {
        scoreDif = Math.Max(0, scoreDif - scoreDifDecay);
    }

    public virtual void DoAction()
    {
        (Vector2Int, int) move = FindBestMove(GridPos, nbTurnForesight);
        Debug.Log(move);

        // Influence de la volonté de déplacement
        Vector2Int moveKey;
        if (move.Item2 >= scoreDif)
        {
            moveKey = move.Item1;
        }
        else
        {
            moveKey = Vector2Int.zero;
        }

        // ============= Aucune action sélectionnée =========== //
        if (!sortedActions.ContainsKey(moveKey))
        {
            Debug.LogWarning($"{moveKey} : Aucune action sélectionnée pour {name}");
            return;
        }

        EnemyActionData action = ChooseAction(moveKey);

        // Déplacement
        Vector2Int oldPos = GridPos;
        Vector2Int newPos = GridPos + moveKey;
        newPos.y %= 2;
        
        if (!BarrierGrid.IsBarrierBroken(GridPos.x) && moveKey.y == 1)
        {
            BarrierGrid.ChangeBarrierState(GridPos.x, BarrierGrid.BarrierState.Destroyed);
        }
        else
        {
            gridComp.ChangePosition(newPos);
        }

        // Échanger avec la potentielle entité sur la destination
        if (EnemyAIControler.IsEnemyOnCell(newPos))
        {
            GameObject entity = EnemyAIControler.GetEnemyOnCell(newPos);

            if (entity != null)
                entity.GetComponent<EntityPosition>().ChangePosition(oldPos);
        }

        GridManager.EnemyGrid.UpdateEntitiesIndex();
        EnemyAIControler.UpdateAllMasks();

        // Execution de l'action
        if (action.isAttack)
        {
            Attack(action);
        }
        else
        {
            Support(action);
        }

        UpdateMoveInitiative();
    }

    // Private
    private (Vector2Int,int) FindBestMove(Vector2Int startCell, int depth)
    {
        UpdateMask();

        int startScore = scoreMask[startCell.x + 3 * startCell.y];

        #region Fin de recherche
        if (depth <= 0)
        {
            return (Vector2Int.zero, startScore);
        }
        #endregion

        // Not moving
        int bestScore = FindBestMove(startCell, --depth).Item2;
        (Vector2Int, int) bestMove = (Vector2Int.zero, 0);

        

        // Balayage de chaque possibilité de déplacement
        foreach (Vector2Int cell in Movements)
        {
            Vector2Int nextCell = startCell + cell;
            nextCell.y %= 2;

            #region Mouvement impossible
            if (!GridManager.EnemyGrid.IsPosInGrid(nextCell))
                continue;
            if (!BarrierGrid.IsBarrierBroken(startCell.x) && cell.y == 1 && !canBreakBarrier)
            {
                continue;
            }
            #endregion

            // Appel récursif pour prévoir sur plusieurs tours
            int currentScore = FindBestMove(nextCell, --depth).Item2 - startScore;

            if (currentScore > bestMove.Item2)
            {
                bestMove = (cell, currentScore);
            }
        }

        return bestMove;
    }

    private EnemyActionData ChooseAction(Vector2Int moveKey)
    {
        var actionList = sortedActions[moveKey];
        int actionWeights = ActionWeightsSum(actionList);

        int rng = UnityEngine.Random.Range(0, actionWeights);
        int currentWeight = 0;

        foreach (var action in actionList)
        {
            currentWeight += action.weight;

            if (rng <= currentWeight)
            {
                return action;
            }
        }
        
        return null;
    }

    private void Attack(EnemyActionData action)
    {
        animHandler.ChangeState(EntityFightAnimation.State.Attack);

        List<GameObject> targets = new List<GameObject>();

        if (action.multipleTarget)
        {
            targets = GridManager.PlayerGrid.GetEntitiesOnRow(gridComp.gridPos.y);
        }
        else
        {
            targets.Add(GridManager.PlayerGrid.GetRandomEntityOnRow(gridComp.gridPos.y));
        }

        foreach (GameObject entity in targets)
        {
            if (entity == null) continue;

            if (action.dmgValue > 0)
            {
                EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
                entityHealth.TakeDamage(gameObject, action.dmgValue, action.dmgType);
            }

            if (action.status != null)
            {
                EntityStatusHolder entityStatus = entity.GetComponent<EntityStatusHolder>();
                entityStatus.AddStatus(action.status, action.statusDuration, gameObject);
            }
        }
    }

    private void Support(EnemyActionData action)
    {
        List<GameObject> targets = new List<GameObject>();
        List<GameObject> possibleTargets;
        switch (action.supportTarget)
        {
            case EnemyActionData.EnemySupportTarget.Self:
                targets.Add(gameObject);
                break;

            case EnemyActionData.EnemySupportTarget.Other:
                possibleTargets = GridManager.EnemyGrid.GetAdjacentEntities(gridComp.gridPos);

                targets.Add(RandomEntityInList(possibleTargets));
                break;

            case EnemyActionData.EnemySupportTarget.SelfOrOther:
                possibleTargets = GridManager.EnemyGrid.GetAdjacentEntities(gridComp.gridPos);
                possibleTargets.Add(gameObject);

                targets.Add(RandomEntityInList(possibleTargets));
                break;

            case EnemyActionData.EnemySupportTarget.Multiple:
                targets = GridManager.EnemyGrid.GetAdjacentEntities(gridComp.gridPos);
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

    private void Start()
    {
        scoreDif = initialScoreDif;

        sortedActions = new Dictionary<Vector2Int, List<EnemyActionData>>();

        foreach (EnemyActionData action in actions)
        {
            if (action == null)
            {
                Debug.LogError($"{name} AI : Invalid Action");
                continue;
            }

            if (sortedActions.ContainsKey(action.posChange))
            {
                sortedActions[action.posChange].Add(action);
            }

            else
            {
                sortedActions.Add(action.posChange, new List<EnemyActionData>() { action });
            }
        }

        /*
        // Debug
        string debugString = $"{name} ::";

        foreach (Vector2Int key in sortedActions.Keys)
        {
            debugString += key + " : ";
            foreach (EnemyActionData action in sortedActions[key])
            {
                debugString += action.name + ", ";
            }

            debugString += " | ";
        }
        Debug.Log(debugString);
        */
    }
}
