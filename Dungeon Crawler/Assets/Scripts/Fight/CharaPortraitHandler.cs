using UnityEngine;
using UnityEngine.UI;

public class CharaPortraitHandler : MonoBehaviour
{
    public static CharaPortraitHandler Instance;

    [SerializeField] private Image portraitHolder;
    [SerializeField] private Sprite defaultSprite;

    public static void ChangePortrait(Sprite portrait)
    {
        Debug.Log(portrait);

        if (portrait == null)
        {
            Instance.portraitHolder.sprite = Instance.defaultSprite;
            Debug.Log("No");
        }
            
        else
            Instance.portraitHolder.sprite = portrait;

        Instance.portraitHolder.SetNativeSize();
    }

    public static void ResetPortait()
    {
        Instance.portraitHolder.sprite = Instance.defaultSprite;

        Instance.portraitHolder.SetNativeSize();
    }

    private void Awake()
    {
        Instance = this;
    }
}
