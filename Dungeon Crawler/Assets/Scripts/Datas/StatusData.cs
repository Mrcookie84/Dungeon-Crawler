using UnityEngine;

public abstract class StatusData : ScriptableObject
{
    [Header("UI")]
    [SerializeField] public Sprite icon;
    
    public virtual void Apply(GameObject entity)
    {
        return;
    }

    public virtual void Tick(GameObject entity)
    {
        return;
    }

    public virtual void Finish(GameObject entity)
    {
        return;
    }

    public virtual void Hit(GameObject entity)
    {
        return;
    }
}
