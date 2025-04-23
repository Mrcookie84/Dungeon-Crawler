using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
        Spawn();
    }

    private void Spawn()
    {
        
        for (int i = 0; i < enemySpawn[0].entityList.Count; i++)
        {
            var enemy = enemySpawn[0].entityList[i];
            var monster = Instantiate(monsterPrefab, ParentEntities);
            ennemyList.Add(monster);
            monster.transform.position = spawn[i].transform.position;
            monster.ApplyData(enemy);
        }

        for (int i = 0; i < playerSpawn[0].playerList.Count; i++) 
        {
            var player = playerSpawn[0].playerList[i];
            var m_player = Instantiate(playerPrefab, ParentEntities);
            playerList.Add(m_player);
            m_player.transform.position = spawn[i].transform.position;
            m_player.ApplyDataPlayer(player);
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
            //ennemy en position 0 
            //calcule les dégats des sorts en fonction des runes 
            //appliquer les dégats : ennemyList[0].pointDeVie -= ...
            //mana -= cout du sort
            //capacityPoints += récupération
            // check si l'ennemy est mort (points de vie <0) => le detruire
                // si l'ennemi est mort => est-ce qu'il y a encore des ennemis ?
                    // si oui => fin du combat
        }
        //else if position ==1
        //else if...


        if (mana == 0)
        {
            fightState = FightState.EnnemyTurn;
        }
        else 
        {
            Array.Clear(runes,0,3);
        }
        //si oui, on reset just la liste des runes choisiés a {0,0,0}
        //si non => change de state pour le tour de l'ennemi
        //fightState = FightState.EnnemyTurn;
    }

    private void EnnemyTurn()
    {
        //applique les effets du sort des monstres
        //vérifier si joueur mort 
        //...
        
        //fin du ennemy turn : repasse fightState = FightState.select rune;
        fightState = FightState.SelectingRune;
    }
    
}
