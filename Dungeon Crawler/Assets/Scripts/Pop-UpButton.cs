using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup; // R�f�rence au pop-up

    private void Start()
    {
        if (popup != null)
        {
            popup.SetActive(false); // Assurez-vous que le pop-up est d�sactiv� au d�marrage
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
            popup.SetActive(false); // Cache le pop-up lorsqu'on arr�te de survoler le bouton
        }
    }
}