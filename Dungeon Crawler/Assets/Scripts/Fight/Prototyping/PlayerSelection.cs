using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private EntityPosition posComponent;
    [SerializeField] private string spellCasterTag;
    private SpellCaster spellCaster;
    [SerializeField] private string charaPortaitTag;
    [SerializeField] private Sprite charaPortraitSprite;
    private PortraitManager charaPortait;

    public UnityEvent playerSelected = new UnityEvent();

    private void Start()
    {
        spellCaster = GameObject.FindGameObjectWithTag(spellCasterTag).GetComponent<SpellCaster>();
        charaPortait = GameObject.FindGameObjectWithTag(charaPortaitTag).GetComponent<PortraitManager>();
        playerSelected.AddListener(delegate { SpellCaster.ChangeCaster(posComponent); });
        playerSelected.AddListener(delegate { charaPortait.ChangeImage(charaPortraitSprite); });
    }

    public void InvokeSelectionEvent()
    {
        playerSelected.Invoke();
    }
}
