using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPageManager : MonoBehaviour
{
    public GameObject[] pages; // Tableau contenant toutes les pages d'aide

    // Méthode appelée par les boutons pour afficher une page spécifique
    public void ShowPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= pages.Length)
        {
            Debug.LogError("Index de page invalide : " + pageIndex);
            return; // Arrêter si l'index est hors des limites
        }

        // Désactiver toutes les pages
        foreach (GameObject page in pages)
        {
            if (page != null)
            {
                page.SetActive(false);
            }
        }

        // Activer uniquement la page sélectionnée
        if (pages[pageIndex] != null)
        {
            pages[pageIndex].SetActive(true);
        }
    }
}