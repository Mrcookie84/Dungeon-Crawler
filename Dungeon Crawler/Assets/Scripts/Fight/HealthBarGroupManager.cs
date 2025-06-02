using UnityEngine;

public class HealthBarGroupManager : MonoBehaviour
{
    [SerializeField] private HealthBarHandler[] healthBars;
    [SerializeField] private GridManager syncGrid;

    public void UpdateHealthBars()
    {
        foreach (HealthBarHandler healthBar in healthBars)
        {
            if (healthBar == null) continue;

            healthBar.SyncSlider(syncGrid);
        }
    }
}
