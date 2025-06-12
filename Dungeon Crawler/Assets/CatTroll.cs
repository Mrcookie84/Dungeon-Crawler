using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTroll : MonoBehaviour
{

    public GameObject CAT;
    
    public void OnMouseDown()
    {
<<<<<<< HEAD
        if (CAT.activeSelf == true)
        {
            StartCoroutine(QUITCOROUT());
        }
        
=======
        CAT.SetActive(false);
        StartCoroutine(QUITCOROUT());
>>>>>>> origin/gd/Thomas
    }

    public IEnumerator QUITCOROUT()
    {
<<<<<<< HEAD
        Debug.LogWarning("Trigger secret ending");
        
        yield return new WaitForSeconds(1);
        
        SceneManager.GoToEnding();
        
=======

        yield return new WaitForSeconds(1);
        
        Debug.LogWarning("SUPOSSED TO QUICK");
        Application.Quit();
>>>>>>> origin/gd/Thomas
    }
    
}
