using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetObject; // L'objet � afficher/masquer

    // Cette fonction sera appel�e par l'�v�nement trigger
    public void ToggleVisibility()
    {
        if (targetObject != null)
        {
            // Inverse l'�tat actif de l'objet
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
