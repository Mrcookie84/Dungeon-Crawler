using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private TurnSubscriber[] subscribers;
    private Stack<TurnSubscriber> turnStack = new Stack<TurnSubscriber>();

    private void Start()
    {
        subscribers = GameObject.FindObjectsByType<TurnSubscriber>(FindObjectsSortMode.None);

        //subscribers.OrderBy((x, y) => x.priority.CompareTo(y.priority));
    }

    private void StartTurn()
    {
        throw new NotImplementedException();
    }

    private void EndTurn()
    {
        throw new NotImplementedException();
    }

    private void NextTurn()
    {
        throw new NotImplementedException();
    }

    private void ResetGlobalTurn()
    {
        turnStack = new Stack<TurnSubscriber>();

        for (int i = 0; i < subscribers.Length; i++)
        {
            turnStack.Push(subscribers[i]);
        }
        
        
    }
}
