using UnityEngine;
using UnityEngine.Events;

public abstract class TurnSubscriber : MonoBehaviour
{
    public UnityEvent endTurnEvent = new UnityEvent();
    [HideInInspector] public bool isPlaying = false;
    
    public abstract void InitializeTurn(TurnManager turnManager);

    public abstract void PlayTurn(TurnManager turnManager);

    public abstract void StatusUpdate();

    public void TurnFinished()
    {
        //Debug.Log($"Fin du tour de {gameObject.name}");
        if (isPlaying)
        {
            StatusUpdate();
            isPlaying = false;
            endTurnEvent.Invoke();
        }
    }
}
