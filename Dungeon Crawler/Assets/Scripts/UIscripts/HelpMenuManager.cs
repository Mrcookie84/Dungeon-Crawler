using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Tr�s important : cette ligne est n�cessaire pour utiliser les �l�ments d'interface utilisateur comme Image et Button

public class HelpImageDisplay : MonoBehaviour
{
    public GameObject[] pages;
    public Image displayImage;       // Le composant Image sur votre Canvas qui va afficher les aides
    public List<Sprite> helpSprites; // La liste de toutes vos images d'aide (vos Sprites)
    public Button page1;    // Le bouton "Pr�c�dent"
    public Button page2;
    public Button page3;
    public Image Page1;
    public Image Page2;
    public GameObject[] buttons; // Liste des boutons correspondant aux pages
    public Image Page3;

    private int currentPageIndex = 0;
        
    void Start()
    {
        // Initialiser l'affichage en activant uniquement la premi�re page
        UpdatePages();
    }
    public void ShowPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= pages.Length)
        {
            Debug.LogError("Index de page invalide : " + pageIndex);
            return; // Arr�ter si l'index est hors des limites
        }

        currentPageIndex = pageIndex; // Mettre � jour l'index de la page active
        UpdatePages(); // Mettre � jour l'affichage
    }

    private void UpdatePages()
    {
        // Parcourir toutes les pages pour activer celle qui correspond � l'index actuel et d�sactiver les autres
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null)
            {
                pages[i].SetActive(i == currentPageIndex); // Active seulement la page correspondante
            }
            else
            {
                Debug.LogWarning("Une page est manquante dans la liste des pages � l'index " + i);
            }
        }
    }
}

