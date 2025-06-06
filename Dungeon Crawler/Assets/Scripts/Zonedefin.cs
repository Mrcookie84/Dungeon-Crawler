using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ImageScroller : MonoBehaviour
{
    public float scrollSpeed = 50f; // Vitesse du défilement
    public string nextSceneName; // Nom de la prochaine scène

    private RectTransform rectTransform;
    private bool isScrolling = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isScrolling)
        {
            // Faire défiler l'image vers le haut
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // Vérifier si l'image est complètement sortie de l'écran
            if (rectTransform.anchoredPosition.y >= rectTransform.rect.height)
            {
                isScrolling = false;
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
