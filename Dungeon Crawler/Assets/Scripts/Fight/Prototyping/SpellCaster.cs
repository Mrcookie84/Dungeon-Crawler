using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCaster : MonoBehaviour
{
    [Header("Turn Component")]
    [SerializeField] private TurnPlayer playerTurn;
    
    [Header("Runes info")]
    [SerializeField] private RuneSelection runeSelection;
    
    [Header("Grid Info")]
    [SerializeField] private GridManager playerGrid;
    [SerializeField] private GridManager enemyGrid;
    [SerializeField] private BarrierGrid barrierGrid;

    [Header("UI")]
    [SerializeField] private GameObject[] UIplayerSelections;
    [SerializeField] private Button UIcastButton;

    public UnityEvent spellCasted = new UnityEvent();

    private CastMode currentCastMode = CastMode.Enemy;
    private EntityPosition casterGPos;
    private SpellEnemyData spellEnemyData;
    private SpellPlayerData spellPlayerData;
    private Vector2Int triggerPos;
    
    public bool HasSpell
    {
        get { return spellEnemyData != null; }
    }


    public enum CastMode
    {
        Player,
        Enemy
    }

    private void ChangeSpell()
    {
        spellEnemyData = Resources.Load<SpellEnemyData>(runeSelection.GetRuneCombinationData());
        Debug.Log(spellEnemyData);
    }

    public void ChangeCaster(EntityPosition casterPos)
    {
        casterGPos = casterPos;

        triggerPos = (Vector2Int)EnemyRaycast(false);
    }

    public void ChangeCastMode()
    {
        // Alterner entre les différents modes d'attaque
        switch (currentCastMode)
        {
            // Possibilité d'ajouter d'autres modes au cas où cette méchanique se développe plus
            case CastMode.Player:
                {
                    currentCastMode = CastMode.Enemy;
                    break;
                }

            case CastMode.Enemy:
                {
                    currentCastMode = CastMode.Player;
                    break;
                }

            // (par défaut : mode ennemi en cas de problème)
            default:
                {
                    currentCastMode = CastMode.Enemy;
                    break;
                }
        }
    }

    private Vector2Int? EnemyRaycast(bool reverseRaycast)
    {
        // Blayage de la grille ennemi depuis l'arrière
        if (reverseRaycast)
        {
            for (int i = 3; i < 0; i--)
            {
                if (enemyGrid.entityList[3 * casterGPos.gridPos.y + i - 1] != null)
                {
                    return new Vector2Int(i, casterGPos.gridPos.y);
                }
            }
        }

        // Balayage de la grille ennemi pour trouver la case sur laquelle activer le sort
        for (int i = 0; i < 3; i++)
        {
            if (enemyGrid.entityList[3* casterGPos.gridPos.y + i] != null)
            {
                return new Vector2Int(i, casterGPos.gridPos.y);
            }
        }

        return null;
    }

    private List<GameObject> GetAllPlayersOnRow()
    {
        List<GameObject> playerList = new List<GameObject>();

        // Balayage de la ligne
        for (int i = 0; i < 3; i++)
        {
            if (playerGrid.entityList[3* casterGPos.gridPos.y + i] != null)
            {
                playerList.Add(playerGrid.entityList[3 * casterGPos.gridPos.y + i]);
            }
        }

        return playerList;
    }

    public void CastSpell()
    {
        runeSelection.UpdateMana();

        switch (currentCastMode)
        {
            case CastMode.Player:
                {
                    CastPlayerSpell();
                    break;
                }

            case CastMode.Enemy:
                {
                    CastEnemySpell();
                    break;
                }
        }
    }

    private void CastPlayerSpell()
    {
        if (spellPlayerData.multipleTargets)
        {
            List<GameObject> affectedPlayers = GetAllPlayersOnRow();

            foreach (GameObject player in affectedPlayers)
            {
                if (spellPlayerData.switchWorld)
                {
                    Debug.Log($"{player.name} change de monde.");
                }
            }
        }
    }

    private void CastEnemySpell()
    {
        for (int i = 0; i < spellEnemyData.hitCellList.Count; i++)
        {
            Vector2Int targetPos = triggerPos + spellEnemyData.hitCellList[i];
            targetPos = new Vector2Int(Mathf.Min(targetPos.x, 3), targetPos.y % 2);
            //Debug.Log(targetPos);

            // Mise à jour de la barrière
            if (spellEnemyData.reinforceBarrier)
            {
                barrierGrid.ChangeBarrierState(targetPos.x, BarrierGrid.BarrierState.Reinforced);
            }
            else if (spellEnemyData.weakenBarrier)
            {
                barrierGrid.ChangeBarrierState(targetPos.x, BarrierGrid.BarrierState.Destroyed);
            }

            if (!(targetPos.y != triggerPos.y && spellEnemyData.blockedByBarrier) || barrierGrid.CheckBarrierState(targetPos.y) == BarrierGrid.BarrierState.Destroyed)
            {
                GameObject hurtEnemy = enemyGrid.GetEntityAtPos(targetPos);
                if (hurtEnemy != null)
                {

                    //Debug.Log($"{hurtEnemy.name} est touché !");

                    // Infliger les dégâts pour chaque type
                    for (int j = 0; j < spellEnemyData.damageTypesData[i].dmgValues.Length; j++)
                    {
                        int dmg = spellEnemyData.damageTypesData[i].dmgValues[j];
                        string dmgType = spellEnemyData.damageTypesData[i].dmgTypeName[j].ToString();
                        hurtEnemy.GetComponent<EntityHealth>().TakeDamage(dmg);
                        //Debug.Log($"{hurtEnemy.name} s'est pris {dmg} dégâts de {dmgType} !");
                    }

                    // Déplacement de l'ennemi
                    EntityPosition enemyPos = hurtEnemy.GetComponent<EntityPosition>();
                    Vector2Int newPos = enemyPos.gridPos + spellEnemyData.displacementList[i];
                    newPos = new Vector2Int(newPos.x, newPos.y % 2);
                    if (enemyGrid.IsPosInGrid(newPos))
                    {
                        enemyPos.ChangePosition(newPos);
                    }
                    else
                    {
                        newPos = enemyPos.gridPos + spellEnemyData.displacementList[i] + spellEnemyData.displacementList[i + 1];
                        newPos = new Vector2Int(newPos.x, newPos.y % 2);
                        if (enemyGrid.IsPosInGrid(newPos))
                        {
                            enemyPos.ChangePosition(newPos);
                        }
                        else
                        {
                            Debug.Log($"{hurtEnemy.name} ne peux pas bouger. ({newPos})");
                        }
                    }
                }
            }
        }

        enemyGrid.UpdateEntitiesIndex();
    }

    public List<Vector2Int> GetAllCellHit()
    {
        List<Vector2Int> hitCellList = new List<Vector2Int>();

        Vector2Int currentCell;
        for (int i = 0; i < spellEnemyData.hitCellList.Count; i++)
        {
            currentCell = triggerPos + spellEnemyData.hitCellList[i];
            hitCellList.Add(currentCell);
        }

        return hitCellList;
    }
}
