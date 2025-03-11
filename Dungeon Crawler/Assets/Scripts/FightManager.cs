using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightManager : MonoBehaviour
{
    public GameObject fightMapPrefab; // La map de combat
    public GameObject[] enemyPrefabs; // Liste des ennemis
    public Transform spawnParent; // Parent des ennemis

    private GameObject fightMapInstance;
    private Vector2[,] gridPositions = new Vector2[2, 3];
    public List<Fighter> fighters = new List<Fighter>(); // Liste des entités
    private Queue<Fighter> turnQueue = new Queue<Fighter>(); // File d'attente pour les tours
    private bool isFightActive = false;

    public Fighter player; // Référence au joueur

    void Start()
    {
        // Initialisation des positions de la grille
        float startX = -1f, startY = 1f, spacingX = 1f, spacingY = 1f;
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                gridPositions[y, x] = new Vector2(startX + (x * spacingX), startY - (y * spacingY));
            }
        }
    }

    public void StartFight()
    {
        if (fightMapInstance == null)
        {
            fightMapInstance = Instantiate(fightMapPrefab, Vector3.zero, Quaternion.identity);
        }
        fightMapInstance.SetActive(true);

        SpawnEnemies();
        SetupTurnOrder();
        StartCoroutine(TurnLoop());
    }

    private void SpawnEnemies()
    {
        List<Vector2> availablePositions = new List<Vector2>();
        foreach (Vector2 pos in gridPositions)
            availablePositions.Add(pos);

        int enemyCount = Random.Range(1, 4);
        for (int i = 0; i < enemyCount; i++)
        {
            if (availablePositions.Count == 0) break;

            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector2 spawnPos = availablePositions[randomIndex];

            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, spawnParent);
            Fighter enemy = enemyObj.GetComponent<Fighter>();
            fighters.Add(enemy); // Ajoute l'ennemi à la liste des combattants

            availablePositions.RemoveAt(randomIndex);
        }
    }

    private void SetupTurnOrder()
    {
        fighters.Add(player); // Ajoute le joueur
        fighters.Sort((a, b) => b.speed.CompareTo(a.speed)); // Tri par vitesse (exemple)
        foreach (Fighter fighter in fighters)
        {
            turnQueue.Enqueue(fighter);
        }
        isFightActive = true;
    }

    private IEnumerator TurnLoop()
    {
        while (isFightActive)
        {
            if (turnQueue.Count == 0)
            {
                SetupTurnOrder();
            }

            Fighter currentFighter = turnQueue.Dequeue();
            yield return StartCoroutine(currentFighter.TakeTurn());

            if (CheckVictory()) yield break;

            turnQueue.Enqueue(currentFighter);
        }
    }

    private bool CheckVictory()
    {
        bool allEnemiesDefeated = fighters.FindAll(f => f.isEnemy && f.isAlive).Count == 0;
        bool playerDefeated = !player.isAlive;

        if (allEnemiesDefeated)
        {
            Debug.Log("Victoire !");
            EndFight();
            return true;
        }
        else if (playerDefeated)
        {
            Debug.Log("Défaite...");
            EndFight();
            return true;
        }
        return false;
    }

    public void EndFight()
    {
        isFightActive = false;
        fightMapInstance.SetActive(false);
        foreach (Transform child in spawnParent)
        {
            Destroy(child.gameObject);
        }
        fighters.Clear();
        turnQueue.Clear();
    }
}
