using UnityEngine;
using UnityEngine.Events;

public class TurnPlayer : TurnSubscriber
{
    public override void PlayTurn(TurnManager turnManager)
    {
        Debug.Log($"Tour de {gameObject.name} commencé !");

        endTurnEvent = new UnityEvent();
        endTurnEvent.AddListener(turnManager.NextTurn);
        endTurnEvent.AddListener(turnManager.DisablePlayerUI);
        isPlaying = true;

        turnManager.EnablePlayerUI();
    }
}
