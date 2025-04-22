using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightManager : MonoBehaviour
{
    
    [Header("List ")]
    [SerializeField]
    private List<Groupe> enemySpawn = new List<Groupe>();
    [SerializeField] 
    private List<GameObject> spawn = new List<GameObject>();
    [SerializeField]
    private List<Groupe> playerSpawn = new List<Groupe>();
    
    [Header("Prefab link")]
    [SerializeField] 
    private Monster monsterPrefab;
    [SerializeField] 
    private Player playerPrefab;
    
    [Header("Entity parent link")]
    [SerializeField] private Transform ParentEntities;
    
    
    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (var i = 0; i < enemySpawn[0].entityList.Count; i++)
        {
            var enemy = enemySpawn[0].entityList[i];
            var monster = Instantiate(monsterPrefab, ParentEntities);
            monster.transform.position = spawn[enemySpawn[0].positionList[i]].transform.position;
            monster.ApplyData(enemy);
        }

        for (int i = 0; i < playerSpawn[0].playerList.Count; i++)
        {
            var player = playerSpawn[0].playerList[i];
            var m_player = Instantiate(playerPrefab, ParentEntities);
            m_player.transform.position = spawn[playerSpawn[0].positionList[i]].transform.position;
            m_player.ApplyDataPlayer(player);
        }
    }

    public void QuitFight()
    {
        SceneManager.GoToRP();
    }
}
