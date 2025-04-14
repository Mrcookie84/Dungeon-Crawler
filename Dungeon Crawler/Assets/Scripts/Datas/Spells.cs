using UnityEngine;

public abstract class Spells : ScriptableObject
{
    [field: Header("Dégats"), SerializeField]
    protected internal int m_degats { get; private set; }

    [field: Header("Zone effect"), SerializeField]
    protected internal float m_zoneEffect { get; private set; }

    public virtual SpellsInstance Instance()
    {
        return new SpellsInstance(this);
    }
}

public class SpellsInstance
{
    public int m_degats;
    public float m_zoneEffect;

    public SpellsInstance(Spells dataSpells)
    {
        m_degats = dataSpells.m_degats;
        m_zoneEffect = dataSpells.m_zoneEffect;
    }
}
