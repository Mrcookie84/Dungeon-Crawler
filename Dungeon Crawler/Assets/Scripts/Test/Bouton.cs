using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPageManager : MonoBehaviour
{
    public GameObject[] pages; // Tableau contenant toutes les pages d'aide

    // M�thode appel�e par les boutons pour afficher une page sp�cifique
    public void ShowPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= pages.Length)
        {
            Debug.LogError("Index de page invalide : " + pageIndex);
            return; // Arr�ter si l'index est hors des limites
        }

        // D�sactiver toutes les pages
        foreach (GameObject page in pages)
        {
            if (page != null)
            {
                page.SetActive(false);
            }
        }

        // Activer uniquement la page s�lectionn�e
        if (pages[pageIndex] != null)
        {
            pages[pageIndex].SetActive(true);
        }
    }
}