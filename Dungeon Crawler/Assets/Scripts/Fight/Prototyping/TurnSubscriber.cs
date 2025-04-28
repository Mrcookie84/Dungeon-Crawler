using UnityEngine;
using UnityEngine.Events;

public abstract class TurnSubscriber : MonoBehaviour
{
    protected UnityEvent endTurnEvent;
    public abstract void PlayTurn(TurnManager turnManager);

    protected void TurnFinished()
    {
        endTurnEvent.Invoke();
    }
}
