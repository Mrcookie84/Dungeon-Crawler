using UnityEngine;

public class RockDetection : MonoBehaviour
{
    public GameObject parentRenard;
    public Rigidbody2D parentRb;

    private bool isHeld = false;

    private void Start()
    {
        parentRb = parentRenard.GetComponent<Rigidbody2D>();
    }

    private void OnMouseUpAsButton()
    {
        if (!isHeld)
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        isHeld = true;
        transform.SetParent(parentRenard.transform);

        if (Mathf.Approximately(parentRb.gravityScale, 1))
            transform.localPosition = new Vector2(0, 1.5f); // Position relative
        else
            transform.localPosition = new Vector2(0, -1.5f);

        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void Drop(Vector3 position)
    {
        isHeld = false;
        transform.SetParent(null);
        transform.position = position;
        GetComponent<Rigidbody2D>().simulated = true;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}