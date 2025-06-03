using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetObject; // L'objet à afficher/masquer

    // Cette fonction sera appelée par l'événement trigger
    public void ToggleVisibility()
    {
        if (targetObject != null)
        {
            // Inverse l'état actif de l'objet
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
