using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Runes")]
public class Runes : Spells
{
    public Sprite imageRunes;

    [field: Header("CostCapacity"), SerializeField]
    protected internal float m_costCapacity { get; private set; }
    
    
    public virtual RunesInstance Instance()
    {
        return new RunesInstance(this);
    }
}

public class RunesInstance
{
    public float m_costCapacity;

    public RunesInstance(Runes dataRunes)
    {
        m_costCapacity = dataRunes.m_costCapacity;
    }
}
