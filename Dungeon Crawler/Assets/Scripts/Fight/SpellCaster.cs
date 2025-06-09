using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpellCaster : MonoBehaviour
{
    public static SpellCaster Instance;
    public static CastMode currentCastMode = CastMode.Enemy;

    private static SpellEnemyData spellEnemyData;
    private static SpellPlayerData spellPlayerData;
    private static EntityPosition casterGPos;
    private static Vector2Int? triggerPos;

    [Header("Turn Component")]
    [SerializeField] private TurnPlayer playerTurn;

    [Header("UI")]
    [SerializeField] private GameObject UIButtonInterface;
    [SerializeField] private GameObject UICastButton;
    [SerializeField] private TMPro.TextMeshProUGUI UISpellDesc;

    [Header("Sound")]
    [SerializeField] private AudioSource spellSE;

    [Space(25)]

    public UnityEvent spellCasted = new UnityEvent();
    
    // =========================== Propriété ============================ //
    public static bool HasSpell
    {
        get { return spellEnemyData != null; }
    }


    // Différents modes de tir
    public enum CastMode
    {
        Player,
        Enemy
    }


    // =========================== Méthodes ============================ //
    private void Awake()
    {
        Instance = this;
    }

    public static void ChangeSpell()
    {
        spellEnemyData = Resources.Load<SpellEnemyData>(RuneSelection.CurrentSpellEnemy);

        spellPlayerData = Resources.Load<SpellPlayerData>(RuneSelection.CurrentSpellPlayer);

        if (casterGPos != null)
        {
            triggerPos = EnemyRaycast(casterGPos.reverseLook);
        }

        UpdateSpell();
    }

    public static void ResetSpell(bool restoreRune = false)
    {
        spellEnemyData = null;
        spellPlayerData = null;

        RuneSelection.ResetSelection(restoreRune);
        UpdateSpell();
    }

    public static void ChangeCaster(EntityPosition casterPos)
    {
        casterGPos = casterPos;
        //Debug.Log($"{casterGPos.gameObject.name} est sélectionné.");

        triggerPos = EnemyRaycast(casterGPos.reverseLook);
        UpdateSpell();
    }

    public static void ResetCaster()
    {
        casterGPos = null;

        triggerPos = null;
        UpdateSpell();
    }

    public static void EnableButtons(bool enable)
    {
        Button[] buttons = Instance.UIButtonInterface.GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            button.interactable = enable;
        }
    }

    public static void ChangeCastMode()
    {
        ResetSpell(true);
        
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

        UpdateSpell();
    }

    private static Vector2Int? EnemyRaycast(bool reverseRaycast)
    {
        // Blayage de la grille ennemi depuis l'arrière
        if (reverseRaycast)
        {
            for (int i = 2; i >= 0; i--)
            {
                if (GridManager.EnemyGrid.entityList[i + 3 * casterGPos.gridPos.y] != null)
                {
                    return new Vector2Int(i, casterGPos.gridPos.y);
                }
            }

            return null;
        }

        // Balayage de la grille ennemi pour trouver la case sur laquelle activer le sort
        for (int i = 0; i < 3; i++)
        {
            if (GridManager.EnemyGrid.entityList[3* casterGPos.gridPos.y + i] != null)
            {
                return new Vector2Int(i, casterGPos.gridPos.y);
            }
        }

        return null;
    }

    private static List<EntityPosition> GetAllPlayersOnRow()
    {
        List<EntityPosition> playerList = new List<EntityPosition>();

        // Balayage de la ligne
        for (int i = 0; i < 3; i++)
        {
            if (GridManager.PlayerGrid.entityList[3* casterGPos.gridPos.y + i] != null)
            {
                playerList.Add(GridManager.PlayerGrid.entityList[3 * casterGPos.gridPos.y + i].GetComponent<EntityPosition>());
            }
        }

        return playerList;
    }

    public void CastSpell()
    {
        EntityFightAnimation casterAnim = casterGPos.gameObject.GetComponent<EntityFightAnimation>();
        casterAnim.ChangeState(EntityFightAnimation.State.Attack);

        spellSE.Play();

        spellCasted.Invoke();

        // Update de la séléction de rune
        RuneSelection.UpdateMana();
        RuneSelection.ResetSelection();

        // Update des grilles
        GridManager.EnemyGrid.ResetHighlight();
        GridManager.PlayerGrid.ResetHighlight();

        // Update UI
        EnableButtons(false);

        if (currentCastMode == CastMode.Enemy)
            StartCoroutine(CastEnemySpellCoroutine(spellEnemyData.SpellDuration));
        else
            StartCoroutine(CastPlayerSpellCoroutine(1f));
    }

    #region Player Spell
    private IEnumerator CastPlayerSpellCoroutine(float t)
    {
        // Liste des entités
        List<EntityPosition> affectedPlayers = new List<EntityPosition>();
        if (spellPlayerData.multipleTargets)
        {
            affectedPlayers = GetAllPlayersOnRow();
        }

        else
        {
            affectedPlayers.Add(casterGPos);
        }

        // Liste des cases
        List<Vector2Int> affectedCells = new List<Vector2Int>();
        foreach (EntityPosition p in affectedPlayers)
        {
            affectedCells.Add(p.gridPos);
        }

        // Lancer les coroutine
        StartCoroutine(CastPlayerFxCoroutine(1f));
        StartCoroutine(CastPlayerEffectCoroutine(1f, affectedPlayers));

        yield return new WaitForSeconds(t);

        EnableButtons(true);
        ResetSpell();
    }

    private IEnumerator CastPlayerEffectCoroutine(float t, List<EntityPosition> affectedPlayers)
    {
        yield return new WaitForSeconds(t);

        foreach (EntityPosition player in affectedPlayers)
        {
            // Changement de position
            if (spellPlayerData.switchWorld)
            {
                player.ChangePosition(new Vector2Int(player.gridPos.x, (player.gridPos.y + 1) % 2));
            }

            // Appliquer du soin
            if (spellPlayerData.healAmount > 0)
            {
                EntityHealth healthComp = player.GetComponent<EntityHealth>();
                healthComp.Heal(spellPlayerData.healAmount);
            }
            
            // Appliquer un statut
            if (spellPlayerData.appliedStatus != null)
            {
                EntityStatusHolder statusComp = player.GetComponent<EntityStatusHolder>();
                statusComp.AddStatus(spellPlayerData.appliedStatus, spellPlayerData.statusDuration, casterGPos.gameObject);
            }
        }

        GridManager.PlayerGrid.UpdateEntitiesIndex();
    }

    private IEnumerator CastPlayerFxCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
    }
    #endregion

    #region Enemy Spell
    private IEnumerator CastEnemySpellCoroutine(float t)
    {   
        // Récupération des cases et ennemis affectés par le sort
        List<Vector2Int> hitCellList = GetAllCellHit(casterGPos.reverseLook);
        GameObject[] enemyArray = GridManager.EnemyGrid.GetEntitiesAtMultPos(hitCellList);

        // Lancement de toutes le coroutines
        StartCoroutine(FxCoroutine(spellEnemyData.t_fx, hitCellList));
        StartCoroutine(DisplacementCoroutine(spellEnemyData.t_disp, enemyArray));
        StartCoroutine(DamageCoroutine(spellEnemyData.t_damage, enemyArray));
        StartCoroutine(BarrierCoroutine(spellEnemyData.t_barrier, hitCellList));

        yield return new WaitForSeconds(5f);

        EnableButtons(true);
        ResetSpell();
    }

    private IEnumerator FxCoroutine(float t, List<Vector2Int> affectedCells)
    {
        // Fx lancer
        GameObject castFx = Instantiate(spellEnemyData.fxCast, casterGPos.transform);
        castFx.GetComponent<FxControler>().SetPointToReach(GridManager.EnemyGrid.GetEntityAtPos(triggerPos.Value).transform.GetChild(0).position);

        yield return new WaitForSeconds(t);

        // Fx impact
        for (int i = 0; i < affectedCells.Count; i++)
        {
            Vector2Int cell = affectedCells[i];
            Transform cellTr = GridManager.EnemyGrid.transform.GetChild(cell.x + 3 * cell.y);

            if (spellEnemyData.fxCell != null)
            {
                GameObject currentFx = Instantiate(spellEnemyData.fxCell, cellTr);
            }
                
        }
    }

    private IEnumerator DisplacementCoroutine(float t, GameObject[] affectedEnemies)
    {
        yield return new WaitForSeconds(t);

        for (int i = 0; i < affectedEnemies.Length; i++)
        {
            if (affectedEnemies[i] == null) continue;

            EntityPosition enemyPos = affectedEnemies[i].GetComponent<EntityPosition>();
            Vector2Int targetPos = enemyPos.gridPos + spellEnemyData.displacementList[i];
            targetPos.y = Mathf.Abs(targetPos.y) % 2;

            bool tryingToCross = spellEnemyData.displacementList[i].y != 0;
            bool barrierBroken = BarrierGrid.IsBarrierBroken(enemyPos.gridPos.x);
            bool targetInGrid = GridManager.EnemyGrid.IsPosInGrid(targetPos);

            if ((!tryingToCross || barrierBroken) && targetInGrid)
            {
                enemyPos.ChangePosition(targetPos);
            }
        }

        EnemyAIControler.UpdateEnemyMask();
    }

    private IEnumerator DamageCoroutine(float t, GameObject[] affectedEnemies)
    {
        // Attente avant déclenchement
        yield return new WaitForSeconds(t);
        
        for (int i = 0; i < affectedEnemies.Length; i++)
        {
            GameObject enemy = affectedEnemies[i];
            
            if (enemy == null) continue;

            EntityHealth entityHealth = enemy.GetComponent<EntityHealth>();

            // Appliquer tout les types de dégâts
            if (spellEnemyData.damageTypesData[i] != null)
                for (int j = 0; j < spellEnemyData.damageTypesData[i].dmgValues.Length; j++)
                {
                    DamageTypesData damageData = spellEnemyData.damageTypesData[i];
                    
                    // Calcule des dégâts
                    int damage = damageData.dmgValues[j];
                    int coef = 100 + PlayerInventory.GetDmgBoost(damageData.dmgTypeName[j]);

                    EntityStatsModifier bonusStats = casterGPos.GetComponent<EntityStatsModifier>();
                    coef += bonusStats.generalAttackBoost;

                    coef = Mathf.Max(0, coef);
                    damage = (int)(damage * (coef / 100f));
                    
                    // Modification de la vie
                    entityHealth.TakeDamage(casterGPos.gameObject, damage, damageData.dmgTypeName[j]);
                }
            
            // Appliquer de potentiels statuts
            EntityStatusHolder entityStatusHolder = enemy.GetComponent<EntityStatusHolder>();

            if (spellEnemyData.statusData != null)
            {
                entityStatusHolder.AddStatus(spellEnemyData.statusData, spellEnemyData.statusDuration, casterGPos.gameObject);
            }
        }
    }

    private IEnumerator BarrierCoroutine(float t, List<Vector2Int> affectedCells)
    {
        yield return new WaitForSeconds(t);

        // Changer l'état de la barrière à chaque case touchée
        foreach (Vector2Int cell in affectedCells)
        {
            if (spellEnemyData.reinforceBarrier)
            {
                BarrierGrid.ChangeBarrierState(cell.x, BarrierGrid.BarrierState.Reinforced);
                // Lancer l'animation
            }
            else if (spellEnemyData.weakenBarrier)
            {
                BarrierGrid.ChangeBarrierState(cell.x, BarrierGrid.BarrierState.Destroyed);
                // Lancer l'animation
            }
        }

        EnemyAIControler.UpdateBarrierMask();
    }
    #endregion

    public static List<Vector2Int> GetAllCellHit(bool reverse)
    {
        List<Vector2Int> hitCellList = new List<Vector2Int>();

        Vector2Int currentCell =  (Vector2Int)triggerPos;
        for (int i = 0; i < spellEnemyData.hitCellList.Count; i++)
        {
            Vector2Int previousCell = currentCell;

            Vector2Int proj = spellEnemyData.hitCellList[i];
            if (reverse)
                proj.x *= -1;

            currentCell = (Vector2Int)triggerPos + proj;
            currentCell.y %= 2; // Remettre la coordonnée y dans le cadriage

            if (GridManager.EnemyGrid.IsPosInGrid(currentCell))
            {
                bool passingThrought = (currentCell.y - previousCell.y) != 0;
                bool barrierBroken = BarrierGrid.IsBarrierBroken(currentCell.x);
                bool canPassThrought = (passingThrought && barrierBroken) || !spellEnemyData.blockedByBarrier;
            
                if ((canPassThrought ||!passingThrought))
                {
                    hitCellList.Add(currentCell);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
            
        }

        return hitCellList;
    }

    #region Spell Update
    private static void UpdateSpell()
    {
        UpdateSpellDesc();
        UpdateSpellPreview();
        UpdateCastButton();
    }

    private static void UpdateCastButton()
    {
        if (casterGPos == null)
        {
            Instance.UICastButton.SetActive(false);
            return;
        }
        
        switch (currentCastMode)
        {
            // Possibilité d'ajouter d'autres modes au cas où cette méchanique se développe plus
            case CastMode.Player:
                {
                    if (spellPlayerData == null)
                    {
                        Instance.UICastButton.SetActive(false);
                    }
                    else
                    {
                        Instance.UICastButton.SetActive(true);
                    }
                    break;
                }

            case CastMode.Enemy:
                {
                    if (spellEnemyData == null || triggerPos == null)
                    {
                        Instance.UICastButton.SetActive(false);
                    }
                    else
                    {
                        Instance.UICastButton.SetActive(true);
                    }
                    break;
                }
        }
    }

    private static void UpdateSpellPreview()
    {
        GridManager.PlayerGrid.ResetHighlight();
        GridManager.EnemyGrid.ResetHighlight();

        if (casterGPos == null)
        {
            return;
        }

        Vector2Int displ;
        switch (currentCastMode)
        {
            
            case CastMode.Player:
                {
                    // Aucun sort
                    if (spellPlayerData == null)
                    {
                        return;
                    }

                    List<CellHighlighter.HighlightInfo> highlightInfoList = new List<CellHighlighter.HighlightInfo>();

                    // Cibles
                    List<EntityPosition> playersPosComponent = new List<EntityPosition>();
                    if (spellPlayerData.multipleTargets)
                    {
                        playersPosComponent = GetAllPlayersOnRow();
                    }
                    else
                    {
                        playersPosComponent.Add(casterGPos);
                    }

                    // Direction
                    if (spellPlayerData.switchWorld)
                        displ = Vector2Int.up;
                    else
                        displ = Vector2Int.zero;

                    foreach (EntityPosition pos in playersPosComponent)
                    {
                        CellHighlighter.HighlightInfo highlightInfo1 = new CellHighlighter.HighlightInfo(pos.gridPos, spellPlayerData.type, displ);
                        highlightInfoList.Add(highlightInfo1);
                    }

                    GridManager.PlayerGrid.HighlightCells(highlightInfoList);

                    break;
                }

            case CastMode.Enemy:
                {
                    // Aucun Sort
                    if (spellEnemyData == null || triggerPos == null)
                    {
                        return;
                    }

                    List<CellHighlighter.HighlightInfo> highlightInfo = new List<CellHighlighter.HighlightInfo>();
                    int i = 0;
                    foreach (var cell in GetAllCellHit(casterGPos.reverseLook))
                    {
                        DamageTypesData.DmgTypes dmgType = spellEnemyData.damageTypesData[i].dmgTypeName[0];
                        displ = spellEnemyData.displacementList[i];

                        var hlInfo = new CellHighlighter.HighlightInfo(cell, dmgType, displ);
                        highlightInfo.Add(hlInfo);

                        i++;
                    }
                    GridManager.EnemyGrid.HighlightCells(highlightInfo);

                    break;
                }
        }
    }

    private static void UpdateSpellDesc()
    {
        switch (currentCastMode)
        {
            case CastMode.Player:
                {
                    if (spellPlayerData == null)
                    {
                        Instance.UISpellDesc.text = "";
                        break;
                    }

                    Instance.UISpellDesc.text = spellPlayerData.desc;

                    break;
                }

            case CastMode.Enemy:
                {
                    if (spellEnemyData == null)
                    {
                        Instance.UISpellDesc.text = "";
                        break;
                    }

                    Instance.UISpellDesc.text = spellEnemyData.desc;
                    break;
                }
        }
    }
    #endregion
}
