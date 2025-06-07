using AYellowpaper.SerializedCollections;
using UnityEngine;

public class CellHighlighter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Vector2Int cellCoords;
    [SerializeField] private SpriteRenderer hlRenderer;
    [SerializeField] private SpriteRenderer arrowRenderer;
    
    [Header("Sprites")]
    [SerializedDictionary("Damage Type", "Sprite")]
    public SerializedDictionary<DamageTypesData.DmgTypes, Sprite> hlSprites;

    [SerializedDictionary("Movement", "Sprite")]
    public SerializedDictionary<Vector2Int, Sprite> arrowSprites;

    public void Highlight(HighlightInfo hlInfo)
    {
        // Debug
        if (!hlSprites.ContainsKey(hlInfo.dmgType))
        {
            Debug.LogWarning($"Highlight : {hlInfo.dmgType} n'est pas dans le dictionnaire des sprites.");
            return;
        }

        if (hlSprites[hlInfo.dmgType] == null)
        {
            Debug.LogWarning($"Highlight : Aucun sprite pour {hlInfo.dmgType}");
            return;
        }
        
        hlRenderer.enabled = true;
        hlRenderer.sprite = hlSprites[hlInfo.dmgType];

        if (hlInfo.displ != Vector2Int.zero)
        {
            //Debug
            if (!arrowSprites.ContainsKey(hlInfo.displ))
            {
                Debug.LogWarning($"Highlight : {hlInfo.displ} n'est pas dans le dictionnaire des sprites.");
                return;
            }

            if (arrowSprites[hlInfo.displ] == null)
            {
                Debug.LogWarning($"Highlight : Aucun sprite pour {hlInfo.displ}");
                return;
            }
            
            arrowRenderer.enabled = true;
            Vector2Int adjustedDispl = hlInfo.displ;
            int verticalDispl = (adjustedDispl + cellCoords).y;
            if (0 > verticalDispl || verticalDispl > 1)
                adjustedDispl.y *= -1;

            arrowRenderer.sprite = arrowSprites[adjustedDispl];
        }
    }

    public void ResetHL()
    {
        hlRenderer.enabled = false;
        arrowRenderer.enabled = false;
    }
    
    public struct HighlightInfo
    {
        public Vector2Int cell;
        public DamageTypesData.DmgTypes dmgType;
        public Vector2Int displ;

        public HighlightInfo(Vector2Int cell, DamageTypesData.DmgTypes dmgType,Vector2Int displ)
        {
            this.cell = cell;
            this.dmgType = dmgType;
            this.displ = displ;
        }
    }
}
