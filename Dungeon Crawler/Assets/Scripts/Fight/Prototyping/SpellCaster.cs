using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private GameObject UIplayerInterface;
    [SerializeField] private GameObject[] UIplayerSelections;
    [SerializeField] private GameObject UIcastButton;
    [SerializeField] private TMPro.TextMeshProUGUI UISpellDesc;

    public UnityEvent spellCasted = new UnityEvent();

    [SerializeField] private CastMode currentCastMode = CastMode.Enemy;
    private EntityPosition casterGPos;
    private SpellEnemyData spellEnemyData;
    private SpellPlayerData spellPlayerData;
    private Vector2Int? triggerPos;
    
    public bool HasSpell
    {
        get { return spellEnemyData != null; }
    }


    public enum CastMode
    {
        Player,
        Enemy
    }

    public void ChangeSpell()
    {
        spellEnemyData = Resources.Load<SpellEnemyData>(runeSelection.GetEnemySpellData());

        spellPlayerData = Resources.Load<SpellPlayerData>(runeSelection.GetPlayerSpellData());

        if (casterGPos != null)
        {
            triggerPos = EnemyRaycast(false);
        }

        UpdateSpell();
    }

    public void ResetSpell()
    {
        spellEnemyData = null;
        spellPlayerData = null;

        UpdateSpell();
    }

    public void ChangeCaster(EntityPosition casterPos)
    {
        casterGPos = casterPos;
        //Debug.Log($"{casterGPos.gameObject.name} est sélectionné.");

        triggerPos = EnemyRaycast(false);
        UpdateSpell();
    }

    public void ChangeCastMode(Image buttonSprite)
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

            return null;
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

    private List<EntityPosition> GetAllPlayersOnRow()
    {
        List<EntityPosition> playerList = new List<EntityPosition>();

        // Balayage de la ligne
        for (int i = 0; i < 3; i++)
        {
            if (playerGrid.entityList[3* casterGPos.gridPos.y + i] != null)
            {
                playerList.Add(playerGrid.entityList[3 * casterGPos.gridPos.y + i].GetComponent<EntityPosition>());
            }
        }

        return playerList;
    }

    /*
    public void CastSpell()
    {
        runeSelection.UpdateMana();

        playerGrid.ResetHighlight();
        enemyGrid.ResetHighlight();

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
    }*/

    private void CastPlayerSpell()
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

        playerGrid.UpdateEntitiesIndex();
    }

    private void CastEnemySpell()
    {
        for (int i = 0; i < spellEnemyData.hitCellList.Count; i++)
        {
            Vector2Int targetPos = (Vector2Int)triggerPos + spellEnemyData.hitCellList[i];
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

            // Barrier check
            bool passingThrought = targetPos.y != ((Vector2Int)triggerPos).y;
            bool canPassThrought = passingThrought && !spellEnemyData.blockedByBarrier;
            bool isBarrierBroken = barrierGrid.CheckBarrierState(targetPos.y) == BarrierGrid.BarrierState.Destroyed;

            bool validTarget = !passingThrought || canPassThrought || isBarrierBroken;

            if (validTarget)
            {
                GameObject hurtEnemy = enemyGrid.GetEntityAtPos(targetPos);
                if (hurtEnemy != null)
                {
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
                    newPos.y %= 2;

                    // Barrier check
                    passingThrought = newPos.y != ((Vector2Int)triggerPos).y;
                    canPassThrought = passingThrought && !spellEnemyData.blockedByBarrier;
                    isBarrierBroken = barrierGrid.CheckBarrierState(newPos.y) == BarrierGrid.BarrierState.Destroyed;

                    validTarget = !passingThrought || canPassThrought || isBarrierBroken;

                    if (enemyGrid.IsPosInGrid(newPos) && (validTarget))
                    {
                        enemyPos.ChangePosition(newPos);
                    }
                    else if (i < spellEnemyData.hitCellList.Count - 1)
                    {
                        newPos = enemyPos.gridPos + spellEnemyData.displacementList[i] + spellEnemyData.displacementList[i + 1];
                        newPos = new Vector2Int(newPos.x, newPos.y % 2);
                        if (enemyGrid.IsPosInGrid(newPos))
                        {
                            enemyPos.ChangePosition(newPos);
                        }
                    }
                }
            }
        }

        enemyGrid.UpdateEntitiesIndex();
    }

    /// <summary>
    /// Méthode appelée par le bouton "Lancer" dans l'interface joueur
    /// C'est ici que le sort est déclenché
    /// </summary>
    public void CastSpell()
    {
        StartCoroutine(CastEnemySpellCoroutine(spellEnemyData.SpellDuration));
    }

    /// <summary>
    /// Coroutine qui lance toutes les autres coroutines qui appliquent les données du sort
    /// - Durée définie par les data de spellEnemyData.SpellDuration
    /// </summary>
    /// <param name="t"> Durée de la coroutine </param>
    /// <returns></returns>
    private IEnumerator CastEnemySpellCoroutine(float t)
    {
        UIplayerInterface.SetActive(false);
        ResetSpell();
        
        // Récupération des cases et ennemis affectés par le sort
        List<Vector2Int> hitCellList = GetAllCellHit();
        GameObject[] enemyArray = enemyGrid.GetEntitiesAtMultPos(hitCellList);

        // Lancement de toutes le coroutines
        StartCoroutine(FxCoroutine(spellEnemyData.t_fx, hitCellList));
        StartCoroutine(DisplacementCoroutine(spellEnemyData.t_disp, enemyArray));
        StartCoroutine(DamageCoroutine(spellEnemyData.t_damage, enemyArray));
        StartCoroutine(BarrierCoroutine(spellEnemyData.t_barrier, hitCellList));

        yield return new WaitForSeconds(t);

        UIplayerInterface.SetActive(true);
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
            
            bool tryingToCross = spellEnemyData.displacementList[i].y != 0;
            bool barrierBroken = barrierGrid.CheckBarrierState(enemyPos.gridPos.x) == BarrierGrid.BarrierState.Destroyed;

            if (!tryingToCross || barrierBroken)
            {
                enemyPos.ChangePosition(enemyPos.gridPos + spellEnemyData.displacementList[i]);
            }
        }
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

        foreach (GameObject enemy in affectedEnemies)
        {
            if (enemy == null) continue;

            EntityHealth entityHealth = enemy.GetComponent<EntityHealth>();

            // Appliquer tout les types de dégâts
            for (int i = 0; i < spellEnemyData.damageTypesData[i].dmgValues.Length; i++)
            {
                entityHealth.TakeDamage(spellEnemyData.damageTypesData[i].dmgValues[i]);

                // Faire apparaître un texte avec la valeur des dégâts
                // et la couleur du type de dégâts infligés
                // (c'est pour ça la boucle for, il y a besoin de l'indice)
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
                barrierGrid.ChangeBarrierState(cell.x, BarrierGrid.BarrierState.Reinforced);
                // Lancer l'animation
            }
            else if (spellEnemyData.weakenBarrier)
            {
                barrierGrid.ChangeBarrierState(cell.x, BarrierGrid.BarrierState.Destroyed);
                // Lancer l'animation
            }
        }
    }

    public List<Vector2Int> GetAllCellHit()
    {
        List<Vector2Int> hitCellList = new List<Vector2Int>();

        Vector2Int currentCell;
        for (int i = 0; i < spellEnemyData.hitCellList.Count; i++)
        {
            currentCell = (Vector2Int)triggerPos + spellEnemyData.hitCellList[i];
            currentCell.y %= 2; // Remettre la coordonnée y dans le cadriage
            hitCellList.Add(currentCell);
        }

        return hitCellList;
    }

    private void UpdateSpell()
    {
        UpdateSpellDesc();
        UpdateSpellPreview();
        UpdateCastButton();
    }

    private void UpdateCastButton()
    {
        if (casterGPos == null)
        {
            UIcastButton.SetActive(false);
            return;
        }
        
        switch (currentCastMode)
        {
            // Possibilité d'ajouter d'autres modes au cas où cette méchanique se développe plus
            case CastMode.Player:
                {
                    if (spellPlayerData == null)
                    {
                        UIcastButton.SetActive(false);
                    }
                    else
                    {
                        UIcastButton.SetActive(true);
                    }
                    break;
                }

            case CastMode.Enemy:
                {
                    if (spellEnemyData == null || triggerPos == null)
                    {
                        UIcastButton.SetActive(false);
                    }
                    else
                    {
                        UIcastButton.SetActive(true);
                    }
                    break;
                }
        }
    }

    private void UpdateSpellPreview()
    {
        playerGrid.ResetHighlight();
        enemyGrid.ResetHighlight();

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

                    playerGrid.HighlightCells(highlightCoords);

                    break;
                }

            case CastMode.Enemy:
                {
                    if (spellEnemyData == null || triggerPos == null)
                    {
                        return;
                    }

                    enemyGrid.HighlightCells(GetAllCellHit());

                    break;
                }
        }
    }

    private void UpdateSpellDesc()
    {
        switch (currentCastMode)
        {
            case CastMode.Player:
                {
                    if (spellPlayerData == null)
                    {
                        UISpellDesc.text = "";
                        break;
                    }

                    UISpellDesc.text = spellPlayerData.desc;

                    break;
                }

            case CastMode.Enemy:
                {
                    if (spellEnemyData == null)
                    {
                        UISpellDesc.text = "";
                        break;
                    }

                    UISpellDesc.text = spellEnemyData.desc;
                    break;
                }
        }
    }
}
