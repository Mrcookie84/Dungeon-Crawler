using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/AI")]
public abstract class AbstractData: ScriptableObject
{
    [Header("Hp"), SerializeField]
    protected int m_hp = 100;
    [Header("Speed"), SerializeField]
    protected float m_speed = 10;
    [Header("Defense"), SerializeField] 
    protected int m_def = 25;


    public virtual AbstractDataInstance Instance()
    {
        return new AbstractDataInstance(this);
    }

    public class AbstractDataInstance
    {
        
        public int m_hp = 100;
        public float m_speed = 10;
        public int m_def = 25;
        
        public AbstractDataInstance(AbstractData data)
        {
            m_hp = data.m_hp;
            m_speed = data.m_speed;
            m_def = data.m_def;
        }
    }
}