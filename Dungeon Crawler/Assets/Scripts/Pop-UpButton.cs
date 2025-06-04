using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup; // Référence au pop-up

    private void Start()
    {
        if (popup != null)
        {
            popup.SetActive(false); // Assurez-vous que le pop-up est désactivé au démarrage
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (popup != null)
        {
            popup.SetActive(true); // Affiche le pop-up lorsqu'on survole le bouton
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (popup != null)
        {
            popup.SetActive(false); // Cache le pop-up lorsqu'on arrête de survoler le bouton
        }
    }
}