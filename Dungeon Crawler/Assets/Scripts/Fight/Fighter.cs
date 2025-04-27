using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Entity entityData;
    private Entity.AbstractDataInstance entityInstanceData;

    public List<Spells> availableSpells;
    private List<SpellsInstance> selectedSpells = new List<SpellsInstance>();

    public int currentMana = 100;
    public int maxMana = 100;

    public bool isDead => entityInstanceData.m_hp <= 0;

    void Start()
    {
        entityInstanceData = entityData.Instance();
    }

    public void SelectSpell(Spells spells)
    {
        if (selectedSpells.Count < 3)
        {
            SpellsInstance spell = spells.Instance();
            selectedSpells.Add(spell);
            Debug.Log(spells.name + " ajouté à la sélection.");
        }
    }

    public void Attack(Fighter target)
    {
        if (selectedSpells.Count > 3)
        {
            Debug.Log("Il ne faut pas sélectionner plus de 3 sorts !");
            return;
        }

        foreach (SpellsInstance spell in selectedSpells)
        {
            if (currentMana >= spell.m_costMana)
            {
                currentMana -= spell.m_costMana;
                target.TakeDamage(spell.m_damage);
                Debug.Log($"{name} lance un sort infligeant {spell.m_damage} dégâts");
            }
            else
            {
                Debug.Log($"{name} n’a pas assez de mana pour ce sort !");
            }
        }

        selectedSpells.Clear();

        if (currentMana <= 0)
        {
            TurnOver();
        }
    }

    public void TakeDamage(int damage)
    {
        entityInstanceData.m_hp -= damage;
        Debug.Log($"{name} a {entityInstanceData.m_hp} HP restants");

        if (entityInstanceData.m_hp <= 0)
        {
            Die();
        }
    }

    public void TurnOver()
    {
        GameManager.Instance.NextTurn();
    }

    public void Die()
    {
        Debug.Log($"{name} est mort !");
        gameObject.SetActive(false);
        GameManager.Instance.CheckEndGame();
    }
}