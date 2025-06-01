using System.Collections;
using UnityEngine;

public class TurnEnemy : TurnSubscriber
{
    [Header("Satus Info")]
    [SerializeField] private EntityStatusHolder statusHolder;
    
    [Header("AI Info")]
    [SerializeField] private EnemyAI ai;

    public override void InitializeTurn(TurnManager turnManager)
    {
        endTurnEvent.AddListener(turnManager.NextTurn);
    }

    public override void PlayTurn(TurnManager turnManager)
    {
        isPlaying = true;
        ai.DoAction();

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        //Debug.Log($"{gameObject.name} attaque !");

        yield return new WaitForSeconds(1);
        
        TurnFinished();
    }

    public override void StatusUpdate()
    {
        statusHolder.UpdateStatus();
    }
}
