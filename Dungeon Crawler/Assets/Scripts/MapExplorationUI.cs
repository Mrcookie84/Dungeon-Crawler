using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapExplorationUI : MonoBehaviour
{

    public void GoToScene(int sceneIndex)
    {
        
        SceneManager.LoadScene(sceneIndex);
        
    }
    
}
