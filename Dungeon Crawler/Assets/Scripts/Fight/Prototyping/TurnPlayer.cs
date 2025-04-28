using UnityEngine;

public class TurnPlayer : TurnSubscriber
{
    public override void PlayTurn(TurnManager turnManager)
    {
        Debug.Log($"Tour de {gameObject.name} commencé !");
        endTurnEvent.AddListener(turnManager.NextTurn);
        endTurnEvent.AddListener(turnManager.DisablePlayerUI);
        
        turnManager.EnablePlayerUI();
    }
}
