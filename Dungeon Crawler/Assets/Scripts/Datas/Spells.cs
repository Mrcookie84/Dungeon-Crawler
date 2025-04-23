using UnityEngine;

public abstract class Spells : ScriptableObject
{
    [field: Header("Damage"), SerializeField]
    protected internal int m_damage { get; private set; }

    [field: Header("Zone effect"), SerializeField]
    protected internal float m_zoneEffect { get; private set; }
    
    [field: Header("CostMana"), SerializeField]
    protected internal int m_costMana { get; private set; }
    
    

    public virtual SpellsInstance Instance()
    {
        return new SpellsInstance(this);
    }
}

public class SpellsInstance
{
    public int m_damage;
    public float m_zoneEffect;
    public int m_costMana;

    public SpellsInstance(Spells dataSpells)
    {
        m_damage = dataSpells.m_damage;
        m_zoneEffect = dataSpells.m_zoneEffect;
        m_costMana = dataSpells.m_costMana;
    }
}
