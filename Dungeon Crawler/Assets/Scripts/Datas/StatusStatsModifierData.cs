using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusStatsModifierData", menuName = "ScriptableObject/Status/StatusStatsModifierData")]
public class StatusStatsModifierData : StatusData
{
    [Header("Stats modifications")]
    public int attackBoost;
    public int resistBoost;

    [Header("Alt")]
    public bool hasAlt;
    public StatusData altStatus;
    public int altDuration;

    public override void Apply(GameObject entity, GameObject source)
    {
        base.Apply(entity, source);

        if (hasAlt)
        {
            EntityPosition posComp = entity.GetComponent<EntityPosition>();
            if (posComp.IsWeak)
            {
                EntityStatusHolder statusComp = entity.GetComponent<EntityStatusHolder>();

                statusComp.AddStatus(altStatus, altDuration, source);
                statusComp.RemoveStatus(this, false);

                statusComp.ForcedUpdate();

                return;
            }
        }

        EntityStatsModifier bonusStats = entity.GetComponent<EntityStatsModifier>();
        bonusStats.generalAttackBoost += attackBoost;
        bonusStats.generalResBoost += resistBoost;
    }

    public override void Finish(GameObject entity)
    {
        base.Finish(entity);

        EntityStatsModifier bonusStats = entity.GetComponent<EntityStatsModifier>();
        bonusStats.generalAttackBoost -= attackBoost;
        bonusStats.generalResBoost -= resistBoost;
    }
}
