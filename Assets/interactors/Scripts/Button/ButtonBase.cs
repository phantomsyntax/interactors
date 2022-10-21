using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class ButtonBase : MonoBehaviour, ICountable
{
                     private BoxCollider2D _boxCollider2D;

    [Header("Button Data Settings")]
    [SerializeField] private SO_ButtonBase _buttonData;
    [SerializeField] private SpriteRenderer _buttonSpriteRenderer;
                     private Color _buttonHoverColor;
                     private Color _buttonClickColor;
                     private bool _isHovering = false;
    [SerializeField] private TextMeshPro _buttonTextCounter;
                     private int _currentClickCount = 0;

    // Start is called before the first frame update
    void Start() {
        // Sprite Renderer
        if(!_buttonSpriteRenderer) {
            _buttonSpriteRenderer = GetComponentInChildren(typeof(SpriteRenderer), true) as SpriteRenderer;
        }
        // Set Sprite image from Scriptable Object
        _buttonSpriteRenderer.sprite = _buttonData.buttonSprite;
        // Set hover color from Scriptable Object
        _buttonHoverColor = _buttonData.hoverColor;
        // Set click color from Scriptable Object
        _buttonClickColor = _buttonData.clickColor;
        
        // Text Counter
        if(!_buttonTextCounter) {
            _buttonTextCounter = GetComponentInChildren(typeof(TextMeshPro), true) as TextMeshPro;
        }

        // Disable the collider on Game Over
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.enabled = true;
        InteractionManager.Instance.OnGameOver.AddListener(() => _boxCollider2D.enabled = false);
    }

    void OnMouseOver() {
        // Change to hover color
        if(!_isHovering) {
            _isHovering = true;
            _buttonSpriteRenderer.color = _buttonHoverColor;
        }
    }

    void OnMouseExit() {
        // Revert to original color
        _isHovering = false;
        _buttonSpriteRenderer.color = Color.white;
    }

    void OnMouseDown() {
        // Change to mouseDown color
        _buttonSpriteRenderer.color = _buttonClickColor;
    }

    void OnMouseUp() {
        if(_isHovering) {
            // Trigger button click event
            InteractionManager.Instance.OnButtonClick?.Invoke();
            // Revert to original color
            _buttonSpriteRenderer.color = _buttonData.hoverColor;
            // Update Text counter
            UpdateCounter();
        } else  {
            _buttonSpriteRenderer.color = Color.white;
        }
    }

    public void UpdateCounter() {
        _currentClickCount++;
        _buttonTextCounter.text = _currentClickCount.ToString();
    }
}
