using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    
    //Faire en sorte de pouvoir set les ennemies utiliser dans le combat sur les trigger 
    
    void Start()
    {
        SceneManager.SceneManagerInstance.combatTrigger = this;
        StartCoroutine(WaitForLoading());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Group"))
        {
            SceneManager.GoToFight();
            Destroy(gameObject);
        }
        
        
    }

    IEnumerator WaitForLoading()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
