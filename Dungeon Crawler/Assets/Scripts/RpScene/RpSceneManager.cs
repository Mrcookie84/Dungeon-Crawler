using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpSceneManager : MonoBehaviour
{
    public void Awake()
    {
        SceneManager.SceneRp = this.gameObject;
    }
}
