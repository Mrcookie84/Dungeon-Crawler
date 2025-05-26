using UnityEngine;

[CreateAssetMenu(fileName = "StatusCoolDownData", menuName = "ScriptableObject/Status/StatusCoolDownData")]
public class StatusCooldownData : StatusData
{
    [Header("Status restored")]
    public StatusData restoredStatus;
    [Min(1)] public int restoredDuration = 1;

    public override void Finish(GameObject entity)
    {
        EntityStatusHolder entityStatusHolder = entity.GetComponent<EntityStatusHolder>();

        entityStatusHolder.AddStatus(restoredStatus, restoredDuration, null);
    }
}
