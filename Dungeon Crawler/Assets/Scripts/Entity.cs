using UnityEngine;


public abstract class Entity: ScriptableObject
{
    [field: Header("Hp"),SerializeField]
    protected int m_hp { get; private set; }

    [field: Header("Speed"),SerializeField]
    protected float m_speed { get; private set; }

    [field: Header("Defense"),SerializeField]
    protected int m_def { get; private set; }


    public virtual AbstractDataInstance Instance()
    {
        return new AbstractDataInstance(this);
    }

    public class AbstractDataInstance
    {

        public int m_hp;
        public float m_speed;
        public int m_def;
        
        public AbstractDataInstance(Entity data)
        {
            m_hp = data.m_hp;
            m_speed = data.m_speed;
            m_def = data.m_def;
        }
    }
}