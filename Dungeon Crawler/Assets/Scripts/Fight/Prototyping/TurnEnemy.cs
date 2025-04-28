using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TurnEnemy : TurnSubscriber
{
    [SerializeField] private EnemyAI ai;

    public override void InitializeTurn(TurnManager turnManager)
    {
        endTurnEvent.AddListener(turnManager.NextTurn);
    }

    public override void PlayTurn(TurnManager turnManager)
    {
        isPlaying = true;
        ai.Attack();

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        //Debug.Log($"{gameObject.name} attaque !");

        yield return new WaitForSeconds(1);
        
        TurnFinished();
    }
}
