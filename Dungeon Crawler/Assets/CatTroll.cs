using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTroll : MonoBehaviour
{

    public GameObject CAT;
    
    public void OnMouseDown()
    {
        if (CAT.activeSelf == true)
        {
            StartCoroutine(QUITCOROUT());
        }
        
    }

    public IEnumerator QUITCOROUT()
    {
        Debug.LogWarning("Trigger secret ending");
        
        yield return new WaitForSeconds(1);
        
        SceneManager.GoToEnding();
        
    }
    
}
