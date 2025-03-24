using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedCombat : MonoBehaviour
{
    public List<Fighter> players;
    public List<Fighter> enemies;
    public Text combatLog;
    public Button attackButton;
    public Button endTurnButton;

    private int currentPlayerIndex = 0;
    private bool isPlayerTurn = true;

    void Start()
    {
        StartPlayerTurn();
        attackButton.onClick.AddListener(PlayerAttack);
        endTurnButton.onClick.AddListener(EndPlayerTurn);
    }

    void StartPlayerTurn()
    {
        combatLog.text = "Tour des joueurs";
        isPlayerTurn = true;
        currentPlayerIndex = 0;
    }

    void PlayerAttack()
    {
        if (isPlayerTurn && enemies.Count > 0)
        {
            Fighter target = enemies[0];
            players[currentPlayerIndex].Attack(target);
            combatLog.text = $"{players[currentPlayerIndex].fighterName} attaque {target.fighterName}!";

            if (!target.isAlive)
            {
                enemies.Remove(target);
                Destroy(target.gameObject);
                CheckWinCondition();
            }

            EndPlayerTurn();
        }
    }

    void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        combatLog.text = "Tour des ennemis";
        yield return new WaitForSeconds(1f);

        foreach (Fighter enemy in enemies)
        {
            if (players.Count > 0)
            {
                Fighter target = players[0];
                enemy.Attack(target);
                combatLog.text = $"{enemy.fighterName} attaque {target.fighterName}!";
                yield return new WaitForSeconds(1f);
                
                if (!target.isAlive)
                {
                    players.Remove(target);
                    Destroy(target.gameObject);
                    CheckWinCondition();
                }
            }
        }
        StartPlayerTurn();
    }

    void CheckWinCondition()
    {
        if (enemies.Count == 0)
        {
            combatLog.text = "Victoire !";
        }
        else if (players.Count == 0)
        {
            combatLog.text = "Défaite...";
        }
    }
}


