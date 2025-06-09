using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Objet représentant le groupe de joueurs")]
    [SerializeField] private GameObject group;

    [Tooltip("Points de spawn associés à chaque niveau")]
    [SerializeField] private List<Transform> spawnPoints;

    [Tooltip("Parents des différents niveaux à activer/désactiver")]
    [SerializeField] private List<GameObject> levelParents;

    private bool canContinue = false;

    private void Start()
    {
        DésactiverTousLesNiveaux();
        if (levelParents.Count > 0)
        {
            levelParents[0].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canContinue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canContinue = false;
        }
    }

    public void UtiliserBouton(int boutonID)
    {
        if (!canContinue)
        {
            Debug.Log("Le groupe doit être dans la zone pour changer de niveau.");
            return;
        }

        if (boutonID < 0 || boutonID >= spawnPoints.Count || boutonID >= levelParents.Count)
        {
            Debug.LogWarning("ID de bouton invalide !");
            return;
        }

        DésactiverTousLesNiveaux();

        levelParents[boutonID].SetActive(true);
        group.transform.position = spawnPoints[boutonID].position;
    }

    private void DésactiverTousLesNiveaux()
    {
        foreach (var level in levelParents)
        {
            if (level != null)
                level.SetActive(false);
        }
    }
}
