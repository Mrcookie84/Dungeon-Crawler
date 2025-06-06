using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class ImageTransition : MonoBehaviour
{
    public RectTransform image1; // Premi�re image (fond)
    public RectTransform image2; // Deuxi�me image (superposition)
    public float scrollSpeed = 100f; // Vitesse de d�filement
    public string nextSceneName; // Nom de la sc�ne suivante
    public float delayBeforeNextScene = 1f; // D�lai avant de charger la sc�ne

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
        // D�filement de `Image1` (fond) vers le haut
        while (image1.anchoredPosition.y < Screen.height)
        {
            image1.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        // D�filement de `Image2` (superposition) vers le haut
        while (image2.anchoredPosition.y < Screen.height)
        {
            image2.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        // Attendre avant de charger la sc�ne
        yield return new WaitForSeconds(delayBeforeNextScene);

        // Charger la sc�ne suivante
        SceneManager.LoadScene(nextSceneName);
    }
}
