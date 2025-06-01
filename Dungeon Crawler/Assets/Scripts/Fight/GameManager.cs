using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Fighter fighter;

    public List<Fighter> playerTeam;
    public List<Fighter> enemyTeam;

    //private int currentFighterIndex = 0;
    private bool playerTurn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        playerTurn = true;
        //currentFighterIndex = 0;
        Debug.Log("Tour des joueurs !");
    }

    public void NextTurn()
    {
        if (playerTurn)
        {
            if (fighter.currentMana <= 0)
            {
                StartCoroutine(EnemyTurn());
            }
        }
    }

    private IEnumerator EnemyTurn()
    {
        playerTurn = false;
        Debug.Log("Tour des ennemis !");
        yield return new WaitForSeconds(1f);

        foreach (var enemy in enemyTeam)
        {
            if (!enemy.isDead)
            {
                Fighter target = playerTeam.Find(p => !p.isDead);
                if (target != null)
                {
                    enemy.Attack(target);
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        yield return new WaitForSeconds(1f);
        StartPlayerTurn();
    }

    public void CheckEndGame()
    {
        bool allPlayersDead = playerTeam.TrueForAll(p => p.isDead);
        bool allEnemiesDead = enemyTeam.TrueForAll(e => e.isDead);

        if (allPlayersDead)
        {
            Debug.Log("Tous les joueurs sont morts. Défaite !");
        }
        else if (allEnemiesDead)
        {
            Debug.Log("Tous les ennemis sont morts. Victoire !");
        }
    }
}