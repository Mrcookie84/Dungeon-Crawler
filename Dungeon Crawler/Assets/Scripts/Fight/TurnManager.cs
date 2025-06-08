using System;
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

    public static void TestEndFight(GridManager grid)
    {
        //Debug.Log("Test fin de combat");
        
        if (grid.IsEmpty)
        {
            PositionManager.EmptyGrids();
            EntityHealth.SaveHealthValue();
            SceneManager.GoToRP();
        }
    }
}
