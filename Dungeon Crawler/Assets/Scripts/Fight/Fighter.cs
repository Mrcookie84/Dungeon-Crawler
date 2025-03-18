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
        return null;
    }

    private void AttackPlayer()
    {
        
    }

    private IEnumerator PlayerAction()
    {
        return null;
    }

    public void TakeDamage(int damage)
    {
        
    }

    private void Die()
    {
        Debug.Log($"{fighterName} est vaincu !");
        gameObject.SetActive(false);
    }
}