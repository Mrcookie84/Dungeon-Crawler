using UnityEngine;
using UnityEngine.UIElements;

public class PortraitManager : MonoBehaviour
{
    [SerializeField] private GameObject portraitholder;

    public void ChangeImage(Sprite sprite)
    {
        if (sprite == null)
        {
            portraitholder.SetActive(false);
        }
        
        portraitholder.GetComponent<Image>().sprite = sprite;
    }
}
