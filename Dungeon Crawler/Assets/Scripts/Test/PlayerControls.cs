using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Right")))
        {
            Debug.Log("Aller à droite");
        }

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Left")))
        {
            Debug.Log("Aller à gauche");
        }

        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Grimoire")))
        {
            Debug.Log("Grimoire ouvert");
        }
        
        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Inventaire")))
        {
            Debug.Log("Inventaire ouvert");
        }
        
        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Map")))
        {
            Debug.Log("Map Ouverte");
        }

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Parametre")))
        {
            Debug.Log("Parametre Ouvert");
        }

        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Interagir")))
        {
            Debug.Log("Vous avez ramassé l'objet");
        }
        
        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Renard")))
        {
            Debug.Log("Vous avez selectionné le Renard");
        }
        
        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Grenouille")))
        {
            Debug.Log("Vous avez selectionné la Grenouille");
        }
    }
}