using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;

    [SerializeField] private List<TurnSubscriber> turnList = new List<TurnSubscriber>();
    private Stack<TurnSubscriber> turnStack = new Stack<TurnSubscriber>();

    int nbEnemy = 0;
    int nbPlayer = 0;

    private void Start()
    {
        // Ajout du joueur
        TurnSubscriber playerTurn = GameObject.FindGameObjectWithTag("Player").GetComponent<TurnSubscriber>();
        playerTurn.InitializeTurn(this);
        turnList.Add(playerTurn);
        nbPlayer = 3;

        // Ajout des tanks
        TurnSubscriber tankIndividualTurn;
        GameObject[] tanksTurn = GameObject.FindGameObjectsWithTag("Tank");
        for (int i = 0; i < tanksTurn.Length; i++)
        {
            tankIndividualTurn = tanksTurn[i].GetComponent<TurnSubscriber>();
            tankIndividualTurn.InitializeTurn(this);    
            turnList.Add(tankIndividualTurn);

            nbEnemy++;
        }
        // Ajout des attaquants
        TurnSubscriber fighterIndividualTurn;
        GameObject[] fightersTurn = GameObject.FindGameObjectsWithTag("Fighter");
        for (int i = 0; i < fightersTurn.Length; i++)
        {
            fighterIndividualTurn = fightersTurn[i].GetComponent<TurnSubscriber>();
            fighterIndividualTurn.InitializeTurn(this);
            turnList.Add(fighterIndividualTurn);

            nbEnemy++;
        }
        // Ajout des supports
        TurnSubscriber supportIndividualTurn;
        GameObject[] supportsTurn = GameObject.FindGameObjectsWithTag("Support");
        for (int i = 0; i < supportsTurn.Length; i++)
        {
            supportIndividualTurn = supportsTurn[i].GetComponent<TurnSubscriber>();
            supportIndividualTurn.InitializeTurn(this);
            turnList.Add(supportIndividualTurn);

            nbEnemy++;
        }

        ResetGlobalTurn();
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

        if (nbEnemy <= 0)
        {
            ; ; ; ; ; ; ; ;
        }

        for (int i = turnList.Count; i > 0; i--)
        {
            turnStack.Push(turnList[i - 1]);
        }

        StartTurn();
    }
    
    public void EnablePlayerUI()
    {
        playerUI.SetActive(true);
    }

    public void DisablePlayerUI()
    {
        playerUI.SetActive(false);
    }
}
