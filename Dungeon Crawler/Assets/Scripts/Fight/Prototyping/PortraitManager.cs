using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    [SerializeField] private GameObject portraitholder;

    public void ChangeImage(Sprite sprite)
    {
        if (sprite == null)
        {
            portraitholder.SetActive(false);
            return;
        }

        portraitholder.SetActive(true);
        portraitholder.GetComponent<Image>().sprite = sprite;
    }
}
