using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class MapUIManagerScript : MonoBehaviour
{
    [SerializeField] public List<Rigidbody2D> playersRb;
    [SerializeField] public RectTransform mapUI;
    
    
    void Start()
    {
        SceneManager.SceneManagerInstance.mapUIManager = this;
    }
    
    public void GoToZone(GameObject zoneSpawn)
    {
        
        for (var i = 0; i < playersRb.Count; i++)
        {
            var playerRb = playersRb[i];
            playerRb.position = new Vector2(zoneSpawn.transform.position.x - i * 2.0f , zoneSpawn.transform.position.y);
                

            if (Mathf.Approximately(playerRb.gravityScale, 1))
            {
                
                playerRb.transform.rotation = Quaternion.RotateTowards(playerRb.transform.rotation, Quaternion.Euler(0, 0, 0), 360f);
            }
            else if (Mathf.Approximately(playerRb.gravityScale, -1))
            {
                playerRb.gravityScale = 1;
                playerRb.transform.rotation = Quaternion.RotateTowards(playerRb.transform.rotation, Quaternion.Euler(0, 0, 0), 360f);
            }
        }
        
        mapUI.DOAnchorPosY(-1227, 1, false).SetEase(Ease.InCubic);
        
    }
    
}
