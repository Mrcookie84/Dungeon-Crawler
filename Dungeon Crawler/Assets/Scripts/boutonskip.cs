using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Butonskipmanager : MonoBehaviour
{
    // M�thode pour changer de sc�ne
    public void ChangeScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName)) // V�rifie que le nom n'est pas vide
        {
            SceneManager.LoadScene(sceneName); // Change la sc�ne
        }
        else
        {
            Debug.LogError("Nom de sc�ne invalide ou vide !");
        }
    }
}