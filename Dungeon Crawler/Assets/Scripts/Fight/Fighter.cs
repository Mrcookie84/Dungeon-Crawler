using System.Collections;
using UnityEngine;

[System.Serializable]


public class Fighter : MonoBehaviour
{
    public Entity entityData;

    public void Attack()
    {
        //Lancer les sorts avec le groupe
    }
    
    public void TakeDamage()
    {
        // pouvoir se prendre des dégats des ennemis
    }

    public void TurnOver()
    {
        // fin du tour, passage aux ennemis 
    }

    public void Die()
    {
        // si les joueurs atteignent 0 de vie il meurt 
    }
    
}