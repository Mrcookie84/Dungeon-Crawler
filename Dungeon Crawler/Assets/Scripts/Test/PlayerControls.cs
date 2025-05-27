using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public Rigidbody2D rigidbody2D;

    public float speed = 10f;


    private Quaternion targetRotation;

    void Update()
    {
        Vector2 force = new Vector2(0, 0);
        
        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Right")))
        {
            force += Vector2.left * speed;
            targetRotation = Quaternion.Euler(0, 180, 0);
            transform.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 0.1f);
        }

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Left")))
        {
            force += Vector2.right * speed;
            targetRotation = Quaternion.Euler(0, 0, 0);
            transform.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 0.1f);
        }
        rigidbody2D.velocity = new Vector2(force.x, rigidbody2D.velocity.y);
    }
}