using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ImageScroller : MonoBehaviour
{
    public float scrollSpeed = 50f; // Vitesse du d�filement
    public string nextSceneName; // Nom de la prochaine sc�ne

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
            // Faire d�filer l'image vers le haut
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // V�rifier si l'image est compl�tement sortie de l'�cran
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
