using UnityEngine;

[CreateAssetMenu(fileName = "EnemyActionData", menuName = "ScriptableObject/EnemyAction/EnemyActionData")]
public class EnemyActionData : ScriptableObject
{
    [Header("Movement")]
    public Vector2Int posChange = Vector2Int.zero;

    [Header("Action Info")]
    public int weight;
    public bool isAttack;

    [Header("Attack")]
    public int dmgValue;
    public DamageTypesData.DmgTypes dmgType;
    public bool multipleTarget;

    [Header("Support")]
    public int healAmount;
    public EnemySupportTarget supportTarget;

    [Header("Status applied")]
    public StatusData status;
    public int statusDuration;

    public enum EnemySupportTarget
    {
        Self,
        Other,
        SelfOrOther,
        Multiple
    }

    private void OnValidate()
    {
        if (posChange.y < 0)
        {
            posChange.y = 0;
        }
        else if (posChange.y > 1)
        {
            posChange.y = 1;
        }
    }
}
