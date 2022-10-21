using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class ButtonBase : MonoBehaviour, ICountable
{
                     private BoxCollider2D boxCollider2D;

    [Header("Button Data Settings")]
    [SerializeField] private SO_ButtonBase buttonData;
    [SerializeField] private SpriteRenderer buttonSpriteRenderer;
                     private Color buttonHoverColor;
                     private Color buttonClickColor;
    [SerializeField] private TextMeshPro buttonTextCounter;
                     private int currentClickCount = 0;

    // Start is called before the first frame update
    void Start() {
        // Sprite Renderer
        if(!buttonSpriteRenderer) {
            buttonSpriteRenderer = GetComponentInChildren(typeof(SpriteRenderer), true) as SpriteRenderer;
        }
        // Set Sprite image from Scriptable Object
        buttonSpriteRenderer.sprite = buttonData.buttonSprite;
        // Set hover color from Scriptable Object
        buttonHoverColor = buttonData.hoverColor;
        // Set click color from Scriptable Object
        buttonClickColor = buttonData.clickColor;
        
        // Text Counter
        if(!buttonTextCounter) {
            buttonTextCounter = GetComponentInChildren(typeof(TextMeshPro), true) as TextMeshPro;
        }

        // Disable the collider on Game Over
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = true;
        InteractionManager.Instance.OnGameOver.AddListener(() => boxCollider2D.enabled = false);
    }

    void OnMouseOver() {
        // Change to hover color
        buttonSpriteRenderer.color = buttonHoverColor;
    }

    void OnMouseExit() {
        // Revert to original color
        buttonSpriteRenderer.color = Color.white;
    }

    void OnMouseDown() {
        // Change to mouseDown color
        buttonSpriteRenderer.color = buttonClickColor;
    }

    void OnMouseUp() {
        // Revert to original color
        buttonSpriteRenderer.color = Color.white;
        // Trigger button click event
        InteractionManager.Instance.OnButtonClick?.Invoke();
        // Update Text counter
        UpdateCounter();
    }

    public void UpdateCounter() {
        currentClickCount++;
        buttonTextCounter.text = currentClickCount.ToString();
    }
}
