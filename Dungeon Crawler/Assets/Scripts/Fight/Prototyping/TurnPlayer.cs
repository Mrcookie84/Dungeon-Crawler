using UnityEngine;

public class TurnPlayer : TurnSubscriber
{
    public override void InitializeTurn(TurnManager turnManager)
    {
        endTurnEvent.AddListener(turnManager.NextTurn);
        endTurnEvent.AddListener(turnManager.DisablePlayerUI);
    }

    public override void PlayTurn(TurnManager turnManager)
    {        
        foreach (Rune rune in RuneSelection.selectedRunes.Keys)
        {
            rune.UpdateCooldown();
        }

        isPlaying = true;

        turnManager.EnablePlayerUI();
    }

    public override void StatusUpdate()
    {
        GameObject[] allPlayerEntities = GameObject.FindGameObjectsWithTag("PlayerEntityFight");
        
        foreach (GameObject playerEntity in allPlayerEntities)
        {
            EntityStatusHolder statusHolder = playerEntity.GetComponent<EntityStatusHolder>();
            
            statusHolder.UpdateStatus();
        }
    }
}
