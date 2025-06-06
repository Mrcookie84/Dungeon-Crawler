using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteGlowOutline : MonoBehaviour
{
    [Tooltip("Couleur du contour")]
    public Color outlineColor = Color.white;

    [Tooltip("Facteur d'agrandissement horizontal du contour")]
    public float outlineWidth = 1.1f;

    [Tooltip("Facteur d'agrandissement vertical du contour")]
    public float outlineHeight = 1.1f;

    [Tooltip("Vitesse d'animation du contour")]
    public float pulseSpeed = 2f;

    [Tooltip("Intensité max du contour (alpha)")]
    public float maxAlpha = 0.8f;

    [Tooltip("Sorting order du contour")]
    public int outlineSortingOrder = 1;

    private GameObject outlineObject;
    private SpriteRenderer outlineRenderer;
    private float alpha;
    private bool increasing = true;
    private Material outlineMaterial;

    void Start()
    {
        SpriteRenderer original = GetComponent<SpriteRenderer>();

        outlineObject = new GameObject("Outline");
        outlineObject.transform.SetParent(transform);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;

        outlineRenderer = outlineObject.AddComponent<SpriteRenderer>();
        outlineRenderer.sprite = original.sprite;
        outlineRenderer.sortingLayerID = original.sortingLayerID;
        outlineRenderer.sortingOrder = outlineSortingOrder;

        Shader spriteShader = Shader.Find("Sprites/Default");
        outlineMaterial = new Material(spriteShader);
        outlineRenderer.material = outlineMaterial;

        outlineRenderer.color = new Color(outlineColor.r, outlineColor.g, outlineColor.b, 0f);

        outlineRenderer.flipX = original.flipX;
        outlineRenderer.flipY = original.flipY;
    }

    void Update()
    {
        outlineObject.transform.localScale = new Vector3(outlineWidth, outlineHeight, 1f);

        float delta = pulseSpeed * Time.deltaTime;

        if (increasing)
        {
            alpha += delta;
            if (alpha >= maxAlpha)
            {
                alpha = maxAlpha;
                increasing = false;
            }
        }
        else
        {
            alpha -= delta;
            if (alpha <= 0f)
            {
                alpha = 0f;
                increasing = true;
            }
        }

        Color c = outlineColor;
        c.a = alpha;
        outlineRenderer.color = c;
    }
}
