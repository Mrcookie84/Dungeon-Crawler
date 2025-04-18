using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> playerList;
    public Spells spell;
    public Entity entityplayer;

    public void ApplySpellToClosestPlayerOnX()
    {
        Transform closestPlayer = null;
        float closestDistanceX = Mathf.Infinity;

        foreach (Transform player in playerList)
        {
            float distanceX = Mathf.Abs(player.position.x - transform.position.x);

            if (distanceX < closestDistanceX)
            {
                closestDistanceX = distanceX;
                closestPlayer = player;
            }
        }

        if (closestPlayer != null)
        {
            // appliquer spellData ou effet au joueur trouvé
            Debug.Log("Ciblage du joueur: " + closestPlayer.name);
            ApplySpell(closestPlayer);
        }
    }

    private void ApplySpell(Transform target)
    {
        // Le système de spell ici (spellData, etc.)
        Debug.Log("Spell appliqué à " + target.name);
    }
}