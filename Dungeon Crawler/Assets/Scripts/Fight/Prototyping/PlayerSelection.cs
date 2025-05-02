using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private EntityPosition posComponent;
    [SerializeField] private string spellCasterTag;
    private SpellCaster spellCaster;

    public UnityEvent playerSelected = new UnityEvent();

    private void Start()
    {
        spellCaster = GameObject.FindGameObjectWithTag(spellCasterTag).GetComponent<SpellCaster>();
        playerSelected.AddListener(delegate { spellCaster.ChangeCaster(posComponent); });
    }

    public void InvokeSelectionEvent()
    {
        playerSelected.Invoke();
    }
}
