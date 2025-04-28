using System.Collections;
using UnityEngine;

public class TurnEnemy : TurnSubscriber
{
    public override void PlayTurn(TurnManager turnManager)
    {
        endTurnEvent.AddListener(turnManager.NextTurn);
    }

    private IEnumerator AttackCoroutine()
    {
        Debug.Log($"{gameObject.name} attaque !");

        yield return new WaitForSeconds(1);
        
        TurnFinished();
    }
}
