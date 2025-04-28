using UnityEngine;
using UnityEngine.Events;

public abstract class TurnSubscriber : MonoBehaviour
{
    protected UnityEvent endTurnEvent = new UnityEvent();
    public bool isPlaying = false;
    
    public abstract void InitializeTurn(TurnManager turnManager);

    public abstract void PlayTurn(TurnManager turnManager);

    public void TurnFinished()
    {
        //Debug.Log($"Fin du tour de {gameObject.name}");
        if (isPlaying)
        {
            isPlaying = false;
            endTurnEvent.Invoke();
        }
    }
}
