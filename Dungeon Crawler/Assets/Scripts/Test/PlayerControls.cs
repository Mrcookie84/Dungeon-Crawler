using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    
    public float speed = 10f;
    
    private KeyCode fwKey = KeyCode.D;
    private KeyCode BwKey = KeyCode.A;


    private Quaternion targetRotation;
    void Update()
    {
        Vector2 force = new Vector2(0, 0);
        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Right")))
        {
            force += Vector2.right * speed;
            targetRotation = Quaternion.Euler(0, 0, 0);
            
            transform.transform.rotation = Quaternion.RotateTowards(((Component)this).transform.rotation, targetRotation, 360f);
        }

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Left")))
        {
            force += Vector2.left * speed;
            targetRotation = Quaternion.Euler(0, 180, 0);
            transform.transform.rotation = Quaternion.RotateTowards(((Component)this).transform.rotation, targetRotation, 360f);
        }

        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Grimoire")))
        {
            Debug.Log("Grimoire ouvert");
        }
        
        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Inventaire")))
        {
            Debug.Log("Inventaire ouvert");
        }
        
        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Map")))
        {
            Debug.Log("Map Ouverte");
        }

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Parametre")))
        {
            Debug.Log("Parametre Ouvert");
        }

        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Interagir")))
        {
            Debug.Log("Vous avez ramassé l'objet");
        }
        
        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Renard")))
        {
            Debug.Log("Vous avez selectionné le Renard");
        }
        
        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Grenouille")))
        {
            Debug.Log("Vous avez selectionné la Grenouille");
        }
    }
}