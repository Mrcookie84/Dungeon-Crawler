using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip anim;
    
    [SerializedDictionary("Damage Type", "Color")]
    public SerializedDictionary<DamageTypesData.DmgTypes, Color> typeColors;

    public IEnumerator DisplayInfo(EntityHealth.DamageInfo dmgInfo)
    {
        damageText.text = dmgInfo.dmgValue.ToString();
        damageText.color = typeColors[dmgInfo.dmgType];

        //animator.Play(anim.name);
        yield return new WaitForSeconds(anim.length);
        
        Destroy(gameObject);
    }
}
