using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private EntityPosition posComponent;
    [SerializeField] private string spellCasterTag;
    private SpellCaster spellCaster;
    [SerializeField] private string charaPortaitTag;
    private Image charaPortait;

    public UnityEvent playerSelected = new UnityEvent();

    private void Start()
    {
        spellCaster = GameObject.FindGameObjectWithTag(spellCasterTag).GetComponent<SpellCaster>();
        charaPortait = GameObject.FindGameObjectWithTag(charaPortaitTag).GetComponent<Image>();
        playerSelected.AddListener(delegate { spellCaster.ChangeCaster(posComponent); });
    }

    public void InvokeSelectionEvent()
    {
        playerSelected.Invoke();
    }
}
