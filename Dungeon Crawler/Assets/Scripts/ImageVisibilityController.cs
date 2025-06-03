using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageVisibilityController : MonoBehaviour
{
    public GameObject imageToShowHide;
    public void ShowImage()
    {
        if (imageToShowHide != null)
        {
            imageToShowHide.SetActive(true);
            Debug.Log("Image rendu visible.");
        }
        
    }
    public void HideImage()
    {

    }


        
    // Start is called before the first frame update
    void Start()
    {
        if (imageToShowHide != null)
        {
            imageToShowHide.SetActive(false);
            Debug.Log("Image rendu invisble");
            
        }
        else if (imageToShowHide != null)
        {
            imageToShowHide.SetActive(false);
            Debug.Log("Image rendu visible");
        }
         void ToggleImageVisibility()
        {
            if (imageToShowHide != null)
            {
                // Inverse l'�tat actif actuel de l'objet
                imageToShowHide.SetActive(!imageToShowHide.activeSelf);
                Debug.Log("Visibilit� de l'image bascul�e. Nouvelle visibilit� : " + imageToShowHide.activeSelf);
            }
        }
    }
}


    // Update is called once per frame
    