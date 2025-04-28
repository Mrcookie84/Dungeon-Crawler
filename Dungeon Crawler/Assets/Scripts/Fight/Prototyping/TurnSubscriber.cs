using UnityEngine;
using UnityEngine.Events;

public abstract class TurnSubscriber : MonoBehaviour
{
    protected UnityEvent endTurnEvent = new UnityEvent();
    public abstract void PlayTurn(TurnManager turnManager);

    public void TurnFinished()
    {
        endTurnEvent.Invoke();
    }
}
