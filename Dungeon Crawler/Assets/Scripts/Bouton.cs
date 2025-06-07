using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonLoader : MonoBehaviour
{
    // Nom de la sc�ne � charger
    public string sceneName = "Scene2";

    // Fonction appel�e par le bouton
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}