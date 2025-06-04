using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private GameObject container;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMPro.TextMeshProUGUI healthValue;

    public void SyncSlider(GridManager referenceGrid)
    {
        GameObject linkedEntity = referenceGrid.entityList[id];
        if (linkedEntity == null)
        {
            container.SetActive(false);
            return;
        }

        if (transform.localScale.y == -1)
        {
            healthValue.transform.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            healthValue.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        EntityHealth healthInfo = linkedEntity.GetComponent<EntityHealth>();

        if (healthInfo.dead)
        {
            container.SetActive(false);
            return;
        }

        healthValue.text = healthInfo.currentHealth.ToString();

        container.SetActive(true);
        healthSlider.maxValue = healthInfo.maxHealth;
        healthSlider.value = healthInfo.currentHealth;
    }
}
