using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Position infos")]
    [SerializeField] private EntityPosition gridComponent;
    [SerializeField] private string playerGridTag;
    private GridManager playerGrid;
    [SerializeField] private string enemyGridTag;
    private GridManager enemyGrid;
    [SerializeField] private string barrierGridTag;
    private BarrierGrid barrierGrid;

    [Header("AI weights")]
    [SerializeField] private int[] columnW = new int[3];
    [Space(5)]
    [SerializeField] private int dimAffinityRow = 0;
    [SerializeField] private int dimAffintyW = 0;
    [Space(5)]
    [SerializeField] private int adjacentEnemyW = 0;
    [Space(5)]
    [SerializeField] private int enemyInWorldW = 0;
    [SerializeField] private int playerInWorldW = 0;
    [Space(5)]
    [SerializeField] private int scoreDif;

    private int currentCellScore = 0;
    private Vector2Int bestCellCoords = new Vector2Int();
    private int bestCellScore = 0;

    private void Start()
    {
        playerGrid = GameObject.FindGameObjectWithTag(playerGridTag).GetComponent<GridManager>();
        enemyGrid = GameObject.FindGameObjectWithTag(enemyGridTag).GetComponent<GridManager>();
        barrierGrid = GameObject.FindGameObjectWithTag(barrierGridTag).GetComponent<BarrierGrid>();
    }

    public void ChooseAction()
    {
        EvaluateAllCells();

        if (bestCellScore - currentCellScore >= scoreDif)
        {
            Debug.Log($"{gameObject.name} se déplace.");
            Move();
        }
        else
        {
            Debug.Log($"{gameObject.name} attaque.");
            Attack();
        }
    }

    private void Move()
    {
        Vector2Int movDirection = FindMoveDirection();
        Vector2Int targetCell = gridComponent.gridPos + movDirection;

        // Vérifier si il essaye de passer la barrière
        if (movDirection.y != 0 && barrierGrid.CheckBarrierState(gridComponent.gridPos.x) == BarrierGrid.BarrierState.Reinforced)
        {
            Attack();
            return;
        }

        // Vérifier si il y a un autre ennemi sur le chemin
        GameObject otherEnemy = enemyGrid.GetEntityAtPos(targetCell);
        if (otherEnemy != null)
        {
            otherEnemy.GetComponent<EntityPosition>().ChangePosition(gridComponent.gridPos);
        }

        List<Vector2Int> list = new List<Vector2Int>();
        list.Add(targetCell);
        enemyGrid.HighlightCells(list);
        gridComponent.ChangePosition(targetCell);

        enemyGrid.UpdateEntitiesIndex();
    }

    private Vector2Int FindMoveDirection()
    {
        int xComp = Math.Sign(bestCellCoords.x - gridComponent.gridPos.x);
        int yComp;
        if (xComp == 0)
        {
            yComp = Math.Sign(bestCellCoords.y - gridComponent.gridPos.y);
        }
        else { yComp = 0; }
        

        return new Vector2Int(xComp, yComp);
    }

    private void Attack()
    {
        GameObject target = ChooseTarget();
        if (target != null)
        {
            Debug.Log($"{target.name} attaquée par {gameObject.name} !");
            target.GetComponent<EntityHealth>().TakeDamage(10);
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

    private void EvaluateAllCells()
    {
        bestCellScore = int.MinValue;
        for(int i = 0;i < 6; i++)
        {
            Vector2Int currentCell = new Vector2Int(i % 3, i / 3);
            int cellScore = EvaluateCell(currentCell);

            if (currentCell == gridComponent.gridPos)
            {
                currentCellScore = cellScore;
            }

            if (cellScore > bestCellScore)
            {
                bestCellCoords = currentCell;
                bestCellScore = cellScore;
            }
        }
    }

    private int EvaluateCell(Vector2Int coords)
    {
        int cellScore = 0;

        // Affinité dimensionnelle
        if (coords.y == dimAffinityRow)
        {
            cellScore += dimAffintyW;
        }

        // Influence de la colone
        cellScore += columnW[coords.x];

        // Placement des ennemis
        int nbEntitiy = enemyGrid.GetAdjacentEntities(gridComponent.gridPos).Count;
        cellScore += nbEntitiy * adjacentEnemyW;

        // Placement dans les mondes
        nbEntitiy = enemyGrid.GetEntitiesOnRow(coords.y).Count;
        cellScore += nbEntitiy * enemyInWorldW;

        nbEntitiy = playerGrid.GetEntitiesOnRow(coords.y).Count;
        cellScore += nbEntitiy * playerInWorldW;

        return cellScore;
    }
}
