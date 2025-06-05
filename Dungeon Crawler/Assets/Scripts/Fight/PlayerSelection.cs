using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private EntityPosition posComponent;
    [SerializeField] private Sprite charaPortraitSprite;

    public void SelectAsCaster()
    {
        SpellCaster.ChangeCaster(posComponent);
        CharaPortraitHandler.ChangePortrait(charaPortraitSprite);
    }
}
