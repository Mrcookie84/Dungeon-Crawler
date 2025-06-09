using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

<<<<<<< HEAD
    [SerializeField] public Texture2D cursorTexture;
=======
    [SerializeField] private Texture2D cursorTexture;
>>>>>>> f746321 (Bug fix)

    private Vector2 cursorHotSpot;
    
    void Start()
    {
        cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture,cursorHotSpot,CursorMode.Auto);
    }

}
