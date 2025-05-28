using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusStunData : StatusData
{
    [Header("Stun parameter")]
    public bool cantMove;
    public bool cantAct;

    public override void Apply(GameObject entity, GameObject source)
    {
        base.Apply(entity, source);
        
        
    }
}
