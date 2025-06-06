using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class ImageTransition : MonoBehaviour
{
    public RectTransform image1; // Première image (fond)
    public RectTransform image2; // Deuxième image (superposition)
    public float scrollSpeed = 100f; // Vitesse de défilement
    public string nextSceneName; // Nom de la scène suivante
    public float delayBeforeNextScene = 1f; // Délai avant de charger la scène

    private bool isTransitioning = false;

    void Update()
    {
        if (!isTransitioning)
        {
            StartTransition();
        }
    }

    private void StartTransition()
    {
        isTransitioning = true;
        StartCoroutine(PerformTransition());
    }

    private IEnumerator PerformTransition()
    {
        // Défilement de `Image1` (fond) vers le haut
        while (image1.anchoredPosition.y < Screen.height)
        {
            image1.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        // Défilement de `Image2` (superposition) vers le haut
        while (image2.anchoredPosition.y < Screen.height)
        {
            image2.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        // Attendre avant de charger la scène
        yield return new WaitForSeconds(delayBeforeNextScene);

        // Charger la scène suivante
        SceneManager.LoadScene(nextSceneName);
    }
}
