using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour
{
    public string fighterName;
    public bool isEnemy;
    public int health = 100;
    public int speed = 10; // Détermine l'ordre du tour
    public bool isAlive => health > 0;

    public IEnumerator TakeTurn()
    {
        Debug.Log($"{fighterName} commence son tour !");
        yield return new WaitForSeconds(1f);

        if (isEnemy)
        {
            AttackPlayer();
        }
        else
        {
            yield return PlayerAction();
        }

        yield return new WaitForSeconds(1f);
    }

    private void AttackPlayer()
    {
        Debug.Log($"{fighterName} attaque le joueur !");
        FindObjectOfType<FightManager>().player.TakeDamage(10);
    }

    private IEnumerator PlayerAction()
    {
        Debug.Log("C'est au tour du joueur ! Choisissez une action.");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // Attente action joueur
        Debug.Log("Le joueur attaque !");
        FindObjectOfType<FightManager>().fighters.Find(f => f.isEnemy && f.isAlive)?.TakeDamage(20);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{fighterName} subit {damage} dégâts. PV restants : {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{fighterName} est vaincu !");
        gameObject.SetActive(false);
    }
}