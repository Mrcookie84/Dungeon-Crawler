using System;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private EntityPosition casterGPos;
    [SerializeField] private string enemyGridTag;
    private GridManager enemyGrid;
    [SerializeField] private string barrierTag;
    private BarrierGrid barrierGrid;
    [SerializeField] private string spellManagerTag;
    private RuneSelection runeSelection;
    [SerializeField] private DamageTypesData damageData;
    [SerializeField] private SpellData spellData;

    private void Start()
    {
        enemyGrid = GameObject.FindGameObjectWithTag(enemyGridTag).GetComponent<GridManager>();
        runeSelection = GameObject.FindGameObjectWithTag(spellManagerTag).GetComponent<RuneSelection>();
        barrierGrid = GameObject.FindGameObjectWithTag(barrierTag).GetComponent<BarrierGrid>();
    }

    public void CastSpell()
    {
        Debug.Log("Sort lancé !");
        ChangeSpell();
        
        // Vérification de la ligne sur laquelle le sort est lancé
        int rowIndex = 0;
        if (casterGPos.gridPos.y == 1)
        {
            rowIndex = 3;
        }
        
        // Trouver le premier ennemi sur la ligne
        bool spellHit = false;
        for (int i = 0; i < 3; i++)
        {
            if (enemyGrid.entityList[rowIndex + i] != null && !spellHit)
            {
                spellHit = true;
                Debug.Log($"Le sort se déclenche sur {enemyGrid.GetEntityAtPos(new Vector2Int(i, casterGPos.gridPos.y)).name} !");
                
                ActivateSpell(new Vector2Int(i, casterGPos.gridPos.y));
            }
        }

        if (!spellHit)
        {
            Debug.Log("Le sort a raté");
        }
    }

    private void ActivateSpell(Vector2Int startPos)
    {
        for (int i = 0; i < spellData.hitCellList.Count; i++)
        {
            Vector2Int targetPos = startPos + spellData.hitCellList[i];
            targetPos = new Vector2Int(Mathf.Min(targetPos.x, 3), targetPos.y % 2);
            //Debug.Log(targetPos);
            
            // Mise à jour de la barrière
            if (spellData.reinforceBarrier)
            {
                barrierGrid.ChangeBarrierState(targetPos.x, BarrierGrid.BarrierState.Reinforced);
            }
            else if (spellData.weakenBarrier)
            {
                barrierGrid.ChangeBarrierState(targetPos.x, BarrierGrid.BarrierState.Destroyed);
            }

            if (!(targetPos.y != startPos.y && spellData.blockedByBarrier) || barrierGrid.CheckBarrierState(targetPos.y) == BarrierGrid.BarrierState.Destroyed)
            {
                GameObject hurtEnemy = enemyGrid.GetEntityAtPos(targetPos);
                if (hurtEnemy != null)
                {
                
                    //Debug.Log($"{hurtEnemy.name} est touché !");
            
                    // Infliger les dégâts pour chaque type
                    for (int j = 0; j < spellData.damageTypesData[i].dmgValues.Length; j++)
                    {
                        int dmg = spellData.damageTypesData[i].dmgValues[j];
                        string dmgType = spellData.damageTypesData[i].dmgTypeName[j].ToString();
                        //Debug.Log($"{hurtEnemy.name} s'est pris {dmg} dégâts de {dmgType} !");
                    }
                    
                    // Déplacement de l'ennemi
                    EntityPosition enemyPos = hurtEnemy.GetComponent<EntityPosition>();
                    Vector2Int newPos = enemyPos.gridPos + spellData.displacementList[i];
                    if (enemyGrid.IsPosInGrid(newPos))
                    {
                        enemyPos.ChangePosition(newPos);
                    }
                    else
                    {
                        newPos += spellData.displacementList[i + 1];
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

    private void ChangeSpell()
    {
        spellData = Resources.Load<SpellData>(runeSelection.GetRuneCombinationData());
        Debug.Log(spellData);
    }
}
