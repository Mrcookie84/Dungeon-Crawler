using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    
    public static TurnManager Instance;

    [SerializeField] private SpellCaster spellCaster;

    [SerializeField] private List<TurnSubscriber> turnList = new List<TurnSubscriber>();
    private Stack<TurnSubscriber> turnStack = new Stack<TurnSubscriber>();

    public static void InitializeTurn()
    {
        // Ajout du joueur
        TurnSubscriber playerTurn = GameObject.FindGameObjectWithTag("PlayerFight").GetComponent<TurnSubscriber>();
        playerTurn.InitializeTurn(Instance);
        Instance.turnList.Add(playerTurn);

        // Ajout des tanks
        TurnSubscriber tankIndividualTurn;
        GameObject[] tanksTurn = GameObject.FindGameObjectsWithTag("Tank");
        for (int i = 0; i < tanksTurn.Length; i++)
        {
            tankIndividualTurn = tanksTurn[i].GetComponent<TurnSubscriber>();
            tankIndividualTurn.InitializeTurn(Instance);
            Instance.turnList.Add(tankIndividualTurn);
        }
        // Ajout des attaquants
        TurnSubscriber fighterIndividualTurn;
        GameObject[] fightersTurn = GameObject.FindGameObjectsWithTag("Fighter");
        for (int i = 0; i < fightersTurn.Length; i++)
        {
            fighterIndividualTurn = fightersTurn[i].GetComponent<TurnSubscriber>();
            fighterIndividualTurn.InitializeTurn(Instance);
            Instance.turnList.Add(fighterIndividualTurn);
        }
        // Ajout des supports
        TurnSubscriber supportIndividualTurn;
        GameObject[] supportsTurn = GameObject.FindGameObjectsWithTag("Support");
        for (int i = 0; i < supportsTurn.Length; i++)
        {
            supportIndividualTurn = supportsTurn[i].GetComponent<TurnSubscriber>();
            supportIndividualTurn.InitializeTurn(Instance);
            Instance.turnList.Add(supportIndividualTurn);
        }

        Instance.ResetGlobalTurn();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void StartTurn()
    {
        TurnSubscriber currentTurn = turnStack.Pop();
        if (currentTurn != null)
        {
            currentTurn.PlayTurn(this);
        }
        else
        {
            NextTurn();
        }
    }

    public void NextTurn()
    {
        EnemyAIControler.UpdateAllMasks();
        
        if (turnStack.Count != 0)
        {
            StartTurn();
        }
        else
        {
            ResetGlobalTurn();
        }
    }

    private void ResetGlobalTurn()
    {
        //Debug.Log("Nouveau tour");
        //turnStack = new Stack<TurnSubscriber>();

        for (int i = turnList.Count; i > 0; i--)
        {
            turnStack.Push(turnList[i - 1]);
        }

        StartTurn();
    }
    
    public void EnablePlayerUI()
    {
        SpellCaster.EnableButtons(true);
    }

    public void DisablePlayerUI()
    {
        SpellCaster.EnableButtons(false);
    }

    public void TestEndFight(GridManager grid)
{
    Debug.Log("Test fin de combat");

    // Coroutine pour détruire TOUTES les entités après les animations
    IEnumerator CleanupAndTransition(bool isVictory)
    {
        // 1. Détruire tous les ennemis (même en victoire/défaite)
        foreach (Transform slot in GridManager.EnemyGrid.transform)
        {
            if (slot.childCount > 1) // Index 1 = l'entité
            {
                Destroy(slot.GetChild(1).gameObject);
            }
        }

        // 2. Détruire les joueurs SEULEMENT en victoire
        if (isVictory)
        {
            foreach (Transform slot in GridManager.PlayerGrid.transform)
            {
                if (slot.childCount > 1)
                {
                    Destroy(slot.GetChild(1).gameObject);
                }
            }
        }

        // 3. Attend que les animations de mort se terminent
        yield return new WaitForSeconds(2f); // Ajustez selon vos animations

        // 4. Transition
        if (isVictory)
        {
            StartCoroutine(GoToRPCorout()); // Retour au RP
        }
        else
        {
            StartCoroutine(DeathCorout());  // Game Over
        }
    }

    // --- Cas 1 : Vérification victoire (tous ennemis morts) ---
    if (grid == GridManager.EnemyGrid)
    {
        bool allEnemiesDead = !GridManager.EnemyGrid.transform.Cast<Transform>()
            .Any(slot => slot.childCount > 1 && 
                        slot.GetChild(1).TryGetComponent<EntityHealth>(out var health) && 
                        !health.dead);

        if (allEnemiesDead)
        {
            StartCoroutine(CleanupAndTransition(true)); // Victory cleanup
        }
    }
    // --- Cas 2 : Vérification défaite (tous joueurs morts) ---
    else if (grid == GridManager.PlayerGrid)
    {
        bool allPlayersDead = !GridManager.PlayerGrid.transform.Cast<Transform>()
            .Any(slot => slot.childCount > 1 && 
                        slot.GetChild(1).TryGetComponent<EntityHealth>(out var health) && 
                        !health.dead);

        if (allPlayersDead)
        {
            StartCoroutine(CleanupAndTransition(false)); // Defeat cleanup
        }
    }
}

    private IEnumerator GoToRPCorout()
    {
        yield return new WaitForSeconds(5f);
        
        SceneManager.GoToRP();
        
    }

    private IEnumerator DeathCorout()
    {
        yield return new WaitForSeconds(1f);
        
        Application.Quit();
    }
}
