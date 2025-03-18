using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer monsterRenderer;

    private MonsterData dataMonster;

    public void ApplyData(MonsterData data)
    {
        dataMonster = data;
        monsterRenderer.sprite = data.imageMonster;
    }
}
