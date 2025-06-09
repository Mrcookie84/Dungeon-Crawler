using UnityEngine;

public class PlaqueDetection : MonoBehaviour
{
    public GameObject door;
    public GameObject openDoor;
    public GameObject leRocher;
    public GameObject leRenard;

    private RockDetection rockScript;

    private void Start()
    {
        rockScript = leRocher.GetComponent<RockDetection>();
    }

    private void OnMouseUpAsButton()
    {
        // Si le renard porte le rocher
        if (rockScript.IsHeld())
        {
            // Poser le rocher sur la plaque
            rockScript.Drop(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == leRocher)
        {
            door.SetActive(false);
            openDoor.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == leRocher)
        {
            door.SetActive(true);
            openDoor.SetActive(false);
        }
    }
}