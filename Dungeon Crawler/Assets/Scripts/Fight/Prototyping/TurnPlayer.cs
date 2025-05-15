using UnityEngine;

public class TurnPlayer : TurnSubscriber
{
    public override void InitializeTurn(TurnManager turnManager)
    {
        endTurnEvent.AddListener(turnManager.NextTurn);
        endTurnEvent.AddListener(turnManager.DisablePlayerUI);

        // Liaison avec le système de mana
        RuneSelection runeSelec = GameManager.FindObjectOfType<RuneSelection>();
        endTurnEvent.AddListener(runeSelec.ResetMana);
    }

    public override void PlayTurn(TurnManager turnManager)
    {
        //Debug.Log($"Tour de {gameObject.name} commencé !");
        
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
