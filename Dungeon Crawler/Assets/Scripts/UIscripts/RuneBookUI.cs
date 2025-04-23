using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;


public class RuneBookUI : MonoBehaviour
{
    [SerializeField] public RectTransform runeBook;
    private bool _RuneBook = false;
    
    
    private void Start()
    {
        runeBook.DOAnchorPosX(-873, 0, false).SetEase(Ease.InCubic);
    }
    
    public void OpenCloseRuneBook()
    {
        
        _RuneBook = !_RuneBook;

        if (_RuneBook == true)
        {
            runeBook.DOAnchorPosX(-873, 0, false).SetEase(Ease.InCubic);
        }
        else
        {
            runeBook.DOAnchorPosX(-543, 0, false).SetEase(Ease.InCubic);
        }
        
    }
}
