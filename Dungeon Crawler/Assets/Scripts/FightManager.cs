using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    private List<Groupe> enemySpawn = new List<Groupe>();
    [SerializeField] 
    private List<GameObject> spawn = new List<GameObject>();
    [SerializeField] 
    private Monster monsterPrefab;
    [SerializeField] 
    private Player playerPrefab;
    [SerializeField]
    private List<Groupe> playerSpawn = new List<Groupe>();

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (var i = 0; i < enemySpawn[0].entityList.Count; i++)
        {
            var enemy = enemySpawn[0].entityList[i];
            var monster = Instantiate(monsterPrefab);
            monster.transform.position = spawn[enemySpawn[0].positionList[i]].transform.position;
            monster.ApplyData(enemy);
        }

        for (int i = 0; i < playerSpawn[0].playerList.Count; i++)
        {
            var player = playerSpawn[0].playerList[i];
            var m_player = Instantiate(playerPrefab);
            m_player.transform.position = spawn[playerSpawn[0].positionList[i]].transform.position;
            m_player.ApplyDataPlayer(player);
        }
    }
}
