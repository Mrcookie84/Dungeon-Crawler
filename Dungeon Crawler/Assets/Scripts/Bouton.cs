using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonLoader : MonoBehaviour
{
    // Nom de la scène à charger
    public string sceneName = "Scene2";

    // Fonction appelée par le bouton
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}