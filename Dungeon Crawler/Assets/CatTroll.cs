using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTroll : MonoBehaviour
{

    public GameObject CAT;
    
    public void OnMouseDown()
    {
        CAT.SetActive(false);
        StartCoroutine(QUITCOROUT());
    }

    public IEnumerator QUITCOROUT()
    {

        yield return new WaitForSeconds(1);
        
        Debug.LogWarning("SUPOSSED TO QUICK");
        Application.Quit();
    }
    
}
