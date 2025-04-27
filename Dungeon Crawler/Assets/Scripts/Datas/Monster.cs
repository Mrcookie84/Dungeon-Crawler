using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer monsterRenderer;

    public MonsterData dataMonster;

    public void ApplyData(MonsterData data)
    {
        dataMonster = data;
        monsterRenderer.sprite = data.imageMonster;
    }
}
