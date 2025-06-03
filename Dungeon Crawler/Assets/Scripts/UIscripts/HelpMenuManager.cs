using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Très important : cette ligne est nécessaire pour utiliser les éléments d'interface utilisateur comme Image et Button

public class HelpImageDisplay : MonoBehaviour
{
    public GameObject[] pages;
    public Image displayImage;       // Le composant Image sur votre Canvas qui va afficher les aides
    public List<Sprite> helpSprites; // La liste de toutes vos images d'aide (vos Sprites)
    public Button page1;    // Le bouton "Précédent"
    public Button page2;
    public Button page3;
    public Image Page1;
    public Image Page2;
    public GameObject[] buttons; // Liste des boutons correspondant aux pages
    public Image Page3;

    private int currentPageIndex = 0;
        
    void Start()
    {
        // Initialiser l'affichage en activant uniquement la première page
        UpdatePages();
    }
    public void ShowPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= pages.Length)
        {
            Debug.LogError("Index de page invalide : " + pageIndex);
            return; // Arrêter si l'index est hors des limites
        }

        currentPageIndex = pageIndex; // Mettre à jour l'index de la page active
        UpdatePages(); // Mettre à jour l'affichage
    }

    private void UpdatePages()
    {
        // Parcourir toutes les pages pour activer celle qui correspond à l'index actuel et désactiver les autres
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null)
            {
                pages[i].SetActive(i == currentPageIndex); // Active seulement la page correspondante
            }
            else
            {
                Debug.LogWarning("Une page est manquante dans la liste des pages à l'index " + i);
            }
        }
    }
}

