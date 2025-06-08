using UnityEngine;

public abstract class StatusData : ScriptableObject
{
    public bool permanent;
    public bool onOff;
    
    [Header("UI")]
    public Sprite icon;

    protected GameObject source;
    
    public virtual void Apply(GameObject entity, GameObject source)
    {
        this.source = source;
        
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

    public virtual void Hit(GameObject entity, EntityHealth.DamageInfo attackInfo)
    {
        return;
    }
}
