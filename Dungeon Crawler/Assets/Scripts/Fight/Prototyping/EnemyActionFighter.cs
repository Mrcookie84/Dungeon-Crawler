using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionFighter : EnemyAction
{
    [Header("Simple Attack")]
    [SerializeField] private int SdmgValue;
    [SerializeField] private DamageTypesData.DmgTypes SdmgType;

    [Header("Group Attack")]
    [SerializeField] private int GdmgValue;
    [SerializeField] private DamageTypesData.DmgTypes GdmgType;
    
    public override void DoAction()
    {
        float rng = Random.value;

        switch (rng)
        {
            case > 0.5f:
                GroupAttack();
                break;
            
            default:
                SimpleAttack();
                break;
        }
    }

    private void SimpleAttack()
    {
        
        List<GameObject> targetList = playerGrid.GetEntitiesOnRow(gridComp.gridPos.y);
        if (targetList.Count == 0){return;}
        
        int targetIndex = Random.Range(0, targetList.Count);
        GameObject target = targetList[targetIndex];

        EntityHealth targetHealth = target.GetComponent<EntityHealth>();
        
        animHandler.ChangeState(EntityFightAnimation.State.Attack);
        targetHealth.TakeDamage(gameObject, SdmgValue, DamageTypesData.DmgTypes.Crush);
    }

    private void GroupAttack()
    {
        List<GameObject> targetList = playerGrid.GetEntitiesOnRow(gridComp.gridPos.y);
        if (targetList.Count == 0){return;}
        
        for (int i = 0; i < targetList.Count; i++)
        {
            GameObject target = targetList[i];

            EntityHealth targetHealth = target.GetComponent<EntityHealth>();
        
            animHandler.ChangeState(EntityFightAnimation.State.Attack);
            targetHealth.TakeDamage(gameObject, GdmgValue, DamageTypesData.DmgTypes.Contact);
        }
    }
}
