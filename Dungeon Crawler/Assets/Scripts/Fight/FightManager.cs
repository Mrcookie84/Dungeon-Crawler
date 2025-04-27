using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour
{
    [SerializeField] private List<Groupe> enemySpawn = new List<Groupe>();
    [SerializeField] private List<GameObject> enemySpawnPosition = new List<GameObject>();
    [SerializeField] private List<Groupe> playerSpawn = new List<Groupe>();
    [SerializeField] private List<GameObject> playerSpawnPosition = new List<GameObject>();
    [SerializeField] private List<TMP_Text> EnnemieHPbarList = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> PlayerHPbarList = new List<TMP_Text>();
    
    [Header("Prefab link")]
    [SerializeField] 
    private Monster monsterPrefab;
    [SerializeField] 
    private Player playerPrefab;

    [SerializeField] private Transform ParentEntities;

    public List<Monster> ennemyList;
    public List<Player> playerList;

    private FightState fightState = FightState.SelectingRune;
    public int mana = 100;
    public int capacityPoints = 10;
    public int[] runes = { 0, 0, 0 };
    private int numberOfRunesAlreadySelected = 0;
    
    public enum FightState
    {
        SelectingRune,
        EnnemyTurn,
    }
        
    private void Start()
    {
        
        foreach (var EnnemieHpbar in EnnemieHPbarList)
        {
            EnnemieHpbar.gameObject.SetActive(false);
        }
        foreach (var PlayerHpbar in PlayerHPbarList)
        {
            PlayerHpbar.gameObject.SetActive(false);
        }
        
        Spawn();
    }

    private void Spawn()
    {
        
        for (int i = 0; i < enemySpawn[0].entityList.Count; i++)
        {
            var enemy = enemySpawn[0].entityList[i];
            var monster = Instantiate(monsterPrefab, ParentEntities);
            
            ennemyList.Add(monster);
            
            monster.transform.position = enemySpawnPosition[i].transform.position;
            monster.ApplyData(enemy);
            
            
            EnnemieHPbarList[i].gameObject.SetActive(true);
            EnnemieHPbarList[i].text = monster.dataMonster.m_hp.ToString();
        }

        for (int i = 0; i < playerSpawn[0].playerList.Count; i++) 
        {
            var player = playerSpawn[0].playerList[i];
            var m_player = Instantiate(playerPrefab, ParentEntities);
            
            playerList.Add(m_player);
            
            m_player.transform.position = playerSpawnPosition[i].transform.position;
            m_player.ApplyDataPlayer(player);
            
            PlayerHPbarList[i].gameObject.SetActive(true);
            PlayerHPbarList[i].text = m_player.dataPlayer.m_hp.ToString();
        }

    }

    public void QuitFight()
    {
        SceneManager.GoToRP();
    }

    private void Update()
    {
        if (fightState == FightState.EnnemyTurn)
        {
            EnnemyTurn();
        }
    }

    public void selectRune(string runeName)
    {
        if (numberOfRunesAlreadySelected == 3)
        {
            Debug.Log("déjà trois runes selectionnées");
        }
        else if (runeName == "Rune1")
        {
            Debug.Log("rune 1 selected");
            if (capacityPoints <= 2)
            {
                Debug.Log("pas assez de capacité");
            }
            else
            {
                capacityPoints -= 3;
                runes[numberOfRunesAlreadySelected] = 1;
                numberOfRunesAlreadySelected++;
            }

            
        }
        else if (runeName == "Rune2")
        {
            Debug.Log("rune 2 selected");
            if (capacityPoints <= 1)
            {
                Debug.Log("pas assez de capacité");
            }
            else
            {
                capacityPoints -= 2;
                runes[numberOfRunesAlreadySelected] = 2;
                numberOfRunesAlreadySelected++;
            }
        }
        else if (runeName == "Rune3")
        {
            Debug.Log("rune 3 selected");
            if (capacityPoints <= 0)
            {
                Debug.Log("pas assez de capacité");
            }
            else
            {
                capacityPoints -= 1;
                runes[numberOfRunesAlreadySelected] = 3;
                numberOfRunesAlreadySelected++;
            }
        }
    }

    public void selectPosition(int EnnemyPosition)
    {
        if (EnnemyPosition == 0)
        {
            Monster target = ennemyList[EnnemyPosition];
            int totalDamage = 0;
            int manaCost = 0;

            foreach (int rune in runes)
            {
                switch (rune)
                {
                    case 1:
                        totalDamage += 10;
                        manaCost += 10;
                        break;
                    case 2:
                        totalDamage += 6;
                        manaCost += 6;
                        break;
                    case 3:
                        totalDamage += 3;
                        manaCost += 3;
                        break;
                }
            }

            target.dataMonster.m_hp -= totalDamage;
            mana -= manaCost;
            capacityPoints += 1;
            if (target.dataMonster.m_hp <= 0)
            {
                Destroy(target.gameObject);
                ennemyList.RemoveAt(EnnemyPosition);
            }

            if (ennemyList.Count == 0)
            { 
                SceneManager.StartingScene = gameObject;
            }
        }
        
        if (EnnemyPosition == 1)
        {
            Monster target = ennemyList[EnnemyPosition];
            int totalDamage = 0;
            int manaCost = 0;

            foreach (int rune in runes)
            {
                switch (rune)
                {
                    case 1:
                        totalDamage += 10;
                        manaCost += 10;
                        break;
                    case 2:
                        totalDamage += 6;
                        manaCost += 6;
                        break;
                    case 3:
                        totalDamage += 3;
                        manaCost += 3;
                        break;
                }
            }

            target.dataMonster.m_hp -= totalDamage;
            mana -= manaCost;
            capacityPoints += 1;
            if (target.dataMonster.m_hp <= 0)
            {
                Destroy(target.gameObject);
                ennemyList.RemoveAt(EnnemyPosition);
            }

            if (ennemyList.Count == 0)
            { 
                SceneManager.StartingScene = gameObject;
            }
        }
        
        if (mana == 0)
        {
            fightState = FightState.EnnemyTurn;
        }
        else 
        {
            Array.Clear(runes,0,3);
        }
    }

    private void EnnemyTurn()
    {
        
            Debug.Log("Tour des ennemis");

            foreach (Monster monster in ennemyList)
            {
                if (playerList.Count == 0) break;
                
                int targetIndex = Random.Range(0, playerList.Count);
                Player targetPlayer = playerList[targetIndex];

                int damage = Random.Range(5, 15); 
                targetPlayer.dataPlayer.m_hp -= damage;

                Debug.Log($"Monstre attaque joueur {targetIndex} pour {damage} dégâts");

                if (targetPlayer.dataPlayer.m_hp <= 0)
                {
                    Debug.Log($"Joueur {targetIndex} est mort !");
                    Destroy(targetPlayer.gameObject);
                    playerList.RemoveAt(targetIndex);
                }
            }

            if (playerList.Count == 0)
            {
                Debug.Log("Tous les joueurs sont morts. Game Over");
                // Tu peux ici ajouter une scène de défaite ou autre action
                QuitFight(); 
                return;
            }

            // Fin du tour des ennemis, retour au joueur
            fightState = FightState.SelectingRune;
            capacityPoints = 10; // on redonne les points de capacité au joueur
    }
}

