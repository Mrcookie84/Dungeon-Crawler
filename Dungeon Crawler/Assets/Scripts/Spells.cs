using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Spells")]
public abstract class Spells : ScriptableObject
{
    [Header("Dégats"), SerializeField] public int m_degats = 10;
    [Header("Zone effect"), SerializeField]
    public float m_zoneEffect = 2;

    public virtual SpellsInstance Instance()
    {
        return new SpellsInstance(this);
    }
}

public class SpellsInstance
{
    public int m_degats = 10;
    public float m_zoneEffect = 2;

    public SpellsInstance(Spells dataSpells)
    {
        m_degats = dataSpells.m_degats;
        m_zoneEffect = dataSpells.m_zoneEffect;
    }
}
