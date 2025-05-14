using UnityEngine;

public abstract class StatusData : MonoBehaviour
{
    public abstract void Tick(GameObject entity);

    public abstract void Finish();
}
