using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TurnEnemy : TurnSubscriber
{
    public override void PlayTurn(TurnManager turnManager)
    {
        endTurnEvent = new UnityEvent();
        endTurnEvent.AddListener(turnManager.NextTurn);
        isPlaying = true;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        Debug.Log($"{gameObject.name} attaque !");

        yield return new WaitForSeconds(1);
        
        TurnFinished();
    }
}
