using System;
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
            triggerPos = EnemyRaycast(false);
        }

        UpdateSpell();
    }

    public static void ResetSpell()
    {
        spellEnemyData = null;
        spellPlayerData = null;

        RuneSelection.ResetSelection();
        UpdateSpell();
    }

    public static void ChangeCaster(EntityPosition casterPos)
    {
        casterGPos = casterPos;
        //Debug.Log($"{casterGPos.gameObject.name} est sélectionné.");

        triggerPos = EnemyRaycast(false);
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

    public static void ChangeCastMode(Image buttonSprite)
    {
        // Alterner entre les différents modes d'attaque
        switch (currentCastMode)
        {
            // Possibilité d'ajouter d'autres modes au cas où cette méchanique se développe plus
            case CastMode.Player:
                {
                    currentCastMode = CastMode.Enemy;
                    buttonSprite.color = Color.red;
                    break;
                }

            case CastMode.Enemy:
                {
                    currentCastMode = CastMode.Player;
                    buttonSprite.color = Color.green;
                    break;
                }

            // (par défaut : mode ennemi en cas de problème)
            default:
                {
                    currentCastMode = CastMode.Enemy;
                    buttonSprite.color = Color.red;
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
            for (int i = 3; i < 0; i--)
            {
                if (EnemyAIControler.EnemyGrid.entityList[3 * casterGPos.gridPos.y + i - 1] != null)
                {
                    return new Vector2Int(i, casterGPos.gridPos.y);
                }
            }

            return null;
        }

        // Balayage de la grille ennemi pour trouver la case sur laquelle activer le sort
        for (int i = 0; i < 3; i++)
        {
            if (EnemyAIControler.EnemyGrid.entityList[3* casterGPos.gridPos.y + i] != null)
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
            if (EnemyAIControler.PlayerGrid.entityList[3* casterGPos.gridPos.y + i] != null)
            {
                playerList.Add(EnemyAIControler.PlayerGrid.entityList[3 * casterGPos.gridPos.y + i].GetComponent<EntityPosition>());
            }
        }

        return playerList;
    }

    private static void CastPlayerSpell()
    {
        if (spellPlayerData.multipleTargets)
        {
            List<EntityPosition> affectedPlayers = GetAllPlayersOnRow();

            foreach (EntityPosition player in affectedPlayers)
            {
                if (spellPlayerData.switchWorld)
                {
                    player.ChangePosition(new Vector2Int(player.gridPos.x, (player.gridPos.y + 1) % 2));
                }
            }
        }

        else
        {
            if (spellPlayerData.switchWorld)
            {
                casterGPos.ChangePosition(new Vector2Int(casterGPos.gridPos.x, (casterGPos.gridPos.y + 1) % 2));
            }
        }

        EnemyAIControler.UpdatePlayerMask();
        EnableButtons(true);
    }

    /// <summary>
    /// Méthode appelée par le bouton "Lancer" dans l'interface joueur
    /// C'est ici que le sort est déclenché
    /// </summary>
    public void CastSpell()
    {
        EntityFightAnimation casterAnim = casterGPos.gameObject.GetComponent<EntityFightAnimation>();
        casterAnim.ChangeState(EntityFightAnimation.State.Attack);

        spellCasted.Invoke();

        // Update de la séléction de rune
        RuneSelection.UpdateMana();
        RuneSelection.ResetSelection();

        // Update des grilles
        EnemyAIControler.EnemyGrid.ResetHighlight();
        EnemyAIControler.PlayerGrid.ResetHighlight();

        // Update UI
        EnableButtons(false);

        if (currentCastMode == CastMode.Enemy)
            StartCoroutine(CastEnemySpellCoroutine(spellEnemyData.SpellDuration));
        else
            CastPlayerSpell(); // Doit être mis à jour plus tard
    }

    /// <summary>
    /// Coroutine qui lance toutes les autres coroutines qui appliquent les données du sort
    /// - Durée définie par les data de spellEnemyData.SpellDuration
    /// </summary>
    /// <param name="t"> Durée de la coroutine </param>
    /// <returns></returns>
    private IEnumerator CastEnemySpellCoroutine(float t)
    {   
        // Récupération des cases et ennemis affectés par le sort
        List<Vector2Int> hitCellList = GetAllCellHit();
        GameObject[] enemyArray = EnemyAIControler.EnemyGrid.GetEntitiesAtMultPos(hitCellList);

        // Lancement de toutes le coroutines
        StartCoroutine(FxCoroutine(spellEnemyData.t_fx, hitCellList));
        StartCoroutine(DisplacementCoroutine(spellEnemyData.t_disp, enemyArray));
        StartCoroutine(DamageCoroutine(spellEnemyData.t_damage, enemyArray));
        StartCoroutine(BarrierCoroutine(spellEnemyData.t_barrier, hitCellList));

        yield return new WaitForSeconds(5f);

        ResetSpell();
        EnableButtons(true);
    }

    /// <summary>
    /// Coroutine qui déclenche les fx du sort lancé au bout d'une durée définie
    /// - Durée définie par spell"""Data.t_fx
    /// </summary>
    /// <param name="t"> Durée de la coroutine </param>
    /// <param name="affectedCells"> Liste des cases où il faut déclencher un fx</param>
    /// <returns></returns>
    private IEnumerator FxCoroutine(float t, List<Vector2Int> affectedCells)
    {
        yield return new WaitForSeconds(t);

        /*
        for (int i = 0; i < affectedCells.Count; i++)
        {
            Vector2Int cell = affectedCells[i];
            Transform cellTr = enemyGrid.transform.GetChild(cell.x + 3 * cell.y);

            switch (spellEnemyData.damageTypesData[i])
            {
                default:
                    ParticleSystem fxHolder = cellTr.GetChild(cellTr.childCount-1).GetComponent<ParticleSystem>();
                    fxHolder.Play();
                    break;
            }
        }*/
        
        // Instantier les fx
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"> Durée de la coroutine </param>
    /// <param name="affectedCells"></param>
    /// <returns></returns>
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
            bool barrierBroken = EnemyAIControler.BarrierGrid.IsBarrierBroken(enemyPos.gridPos.x);
            bool targetInGrid = EnemyAIControler.EnemyGrid.IsPosInGrid(targetPos);

            if ((!tryingToCross || barrierBroken) && targetInGrid)
            {
                enemyPos.ChangePosition(targetPos);
            }
        }

        EnemyAIControler.UpdateEnemyMask();
    }

    /// <summary>
    /// Coroutine qui déclenche l'application des dégâts sur les ennemis affectés
    /// </summary>
    /// <param name="t"> Durée de la coroutine </param>
    /// <param name="affectedEnemies"> Liste des ennemis</param>
    /// <returns></returns>
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
                    // Modification de la vie
                    DamageTypesData damageData = spellEnemyData.damageTypesData[i];
                    entityHealth.TakeDamage(casterGPos.gameObject, damageData.dmgValues[j], damageData.dmgTypeName[j]);
                }
            
            // Appliquer de potentiels statuts
            EntityStatusHolder entityStatusHolder = enemy.GetComponent<EntityStatusHolder>();

            if (spellEnemyData.statusData != null)
            {
                entityStatusHolder.AddStatus(spellEnemyData.statusData, spellEnemyData.statusDuration, casterGPos.gameObject);
            }
        }
    }

    /// <summary>
    /// Coroutine qui définie quand la barrière est mise à jour
    /// - Durée définie par spell"""Data.t_barrier
    /// </summary>
    /// <param name="t"> Durée de la coroutine </param>
    /// <param name="affectedCells"> Liste des cases où l'état de la barrière est mis à jour </param>
    /// <returns></returns>
    private IEnumerator BarrierCoroutine(float t, List<Vector2Int> affectedCells)
    {
        yield return new WaitForSeconds(t);

        // Changer l'état de la barrière à chaque case touchée
        foreach (Vector2Int cell in affectedCells)
        {
            if (spellEnemyData.reinforceBarrier)
            {
                EnemyAIControler.BarrierGrid.ChangeBarrierState(cell.x, BarrierGrid.BarrierState.Reinforced);
                // Lancer l'animation
            }
            else if (spellEnemyData.weakenBarrier)
            {
                EnemyAIControler.BarrierGrid.ChangeBarrierState(cell.x, BarrierGrid.BarrierState.Destroyed);
                // Lancer l'animation
            }
        }

        EnemyAIControler.UpdateBarrierMask();
    }

    public static List<Vector2Int> GetAllCellHit()
    {
        List<Vector2Int> hitCellList = new List<Vector2Int>();

        Vector2Int currentCell =  (Vector2Int)triggerPos;
        for (int i = 0; i < spellEnemyData.hitCellList.Count; i++)
        {
            Vector2Int previousCell = currentCell;
            
            currentCell = (Vector2Int)triggerPos + spellEnemyData.hitCellList[i];
            currentCell.y %= 2; // Remettre la coordonnée y dans le cadriage

            if (EnemyAIControler.EnemyGrid.IsPosInGrid(currentCell))
            {
                bool passingThrought = (currentCell.y - previousCell.y) != 0;
                bool barrierBroken = EnemyAIControler.BarrierGrid.IsBarrierBroken(currentCell.x);
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
        EnemyAIControler.PlayerGrid.ResetHighlight();
        EnemyAIControler.EnemyGrid.ResetHighlight();

        if (casterGPos == null)
        {
            return;
        }

        switch (currentCastMode)
        {
            case CastMode.Player:
                {
                    if (spellPlayerData == null)
                    {
                        return;
                    }

                    List<Vector2Int> highlightCoords = new List<Vector2Int>();

                    if (spellPlayerData.multipleTargets)
                    {
                        List<EntityPosition> playersPosComponent = GetAllPlayersOnRow();

                        foreach (EntityPosition posComp in playersPosComponent)
                        {
                            highlightCoords.Add(posComp.gridPos);
                        }
                    }
                    else
                    {
                        highlightCoords.Add(casterGPos.gridPos);
                    }

                    EnemyAIControler.PlayerGrid.HighlightCells(highlightCoords);

                    break;
                }

            case CastMode.Enemy:
                {
                    if (spellEnemyData == null || triggerPos == null)
                    {
                        return;
                    }

                    EnemyAIControler.EnemyGrid.HighlightCells(GetAllCellHit());

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
}
