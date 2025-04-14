using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneManager : MonoBehaviour
{
    public void Awake()
    {
        SceneManager.sceneFight = gameObject;
    }
}
