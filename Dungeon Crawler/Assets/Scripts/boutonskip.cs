using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Butonskipmanager : MonoBehaviour
{
    // Méthode pour changer de scène
    public void ChangeScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName)) // Vérifie que le nom n'est pas vide
        {
            SceneManager.LoadScene(sceneName); // Change la scène
        }
        else
        {
            Debug.LogError("Nom de scène invalide ou vide !");
        }
    }
}