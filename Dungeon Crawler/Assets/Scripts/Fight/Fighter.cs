using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public string fighterName;
    public int health = 100;
    public int attackPower = 20;
    public bool isPlayer;
    public Fighter player;
    public Fighter enemy;
    
    private void Start()
    {
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        while (player.health > 0 && enemy.health > 0)
        {
            yield return new WaitForSeconds(1);
            player.Attack(enemy);
            
            if (enemy.health <= 0)
                break;
            
            yield return new WaitForSeconds(1);
            enemy.Attack(player);
        }
        Debug.Log("Combat terminé!");
    }

    public void Attack(Fighter target)
    {
        Debug.Log(fighterName + " attaque " + target.fighterName + " et inflige " + attackPower + " dégâts!");
        target.TakeDamage(attackPower);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(fighterName + " a maintenant " + health + " points de vie.");
        
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(fighterName + " est vaincu!");
    }
}


   

    
