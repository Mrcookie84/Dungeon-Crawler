using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;
    
    private List<TurnSubscriber> turnList = new List<TurnSubscriber>();
    private Stack<TurnSubscriber> turnStack = new Stack<TurnSubscriber>();

    private void Start()
    {
        // Ajout du joueur
        TurnSubscriber playerTurn = GameObject.FindGameObjectWithTag("Player").GetComponent<TurnSubscriber>();
        turnList.Add(playerTurn);
        
        // Ajout des tanks
        GameObject[] tanksTurn = GameObject.FindGameObjectsWithTag("Tank");
        for (int i = 0; i < tanksTurn.Length; i++)
        {
            turnList.Add(tanksTurn[i].GetComponent<TurnSubscriber>());
        }
        // Ajout des attaquants
        GameObject[] fighterTurn = GameObject.FindGameObjectsWithTag("Fighter");
        for (int i = 0; i < fighterTurn.Length; i++)
        {
            turnList.Add(fighterTurn[i].GetComponent<TurnSubscriber>());
        }
        // Ajout des supports
        GameObject[] supportTurn = GameObject.FindGameObjectsWithTag("Support");
        for (int i = 0; i < supportTurn.Length; i++)
        {
            turnList.Add(supportTurn[i].GetComponent<TurnSubscriber>());
        }
        
        ResetGlobalTurn();
    }

    private void StartTurn()
    {
        TurnSubscriber currentTurn = turnStack.Pop();
        currentTurn.PlayTurn(this);
    }

    public void NextTurn()
    {
        if (turnStack.TryPeek(out TurnSubscriber dontcare))
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
        turnStack = new Stack<TurnSubscriber>();

        for (int i = turnList.Count; i > 0; i--)
        {
            turnStack.Push(turnList[i - 1]);
        }
        
        StartTurn();
    }

    public void EndPlayerTurn()
    {
        DisablePlayerUI();
        NextTurn();
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
