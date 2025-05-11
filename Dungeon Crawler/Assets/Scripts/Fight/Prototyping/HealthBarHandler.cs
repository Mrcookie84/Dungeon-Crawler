using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private GameObject container;
    [SerializeField] private Slider healthSlider;

    public void SyncSlider(GridManager referenceGrid)
    {
        GameObject linkedEntity = referenceGrid.entityList[id];
        if (linkedEntity == null)
        {
            container.SetActive(false);
            return;
        }

        EntityHealth healthInfo = linkedEntity.GetComponent<EntityHealth>();

        if (healthInfo.dead)
        {
            container.SetActive(false);
            return;
        }

        container.SetActive(true);
        healthSlider.maxValue = healthInfo.maxHealth;
        healthSlider.value = healthInfo.currentHealth;
    }
}
