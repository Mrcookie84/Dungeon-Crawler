using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private EntityPosition posComponent;
    [SerializeField] private EntityHealth healthComponent;
    [SerializeField] private Sprite charaPortraitSprite;

    public void SelectAsCaster()
    {
        if (healthComponent.dead) return;

        Debug.Log("Allo");

        SpellCaster.ChangeCaster(posComponent);
        CharaPortraitHandler.ChangePortrait(charaPortraitSprite);
    }
}
