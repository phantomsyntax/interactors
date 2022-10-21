using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchBase : MonoBehaviour, ICountable
{
                     private BoxCollider2D _boxCollider2D;
    [Header("Switch Data Settings")]
    [SerializeField] private SO_SwitchBase _switchData;
    [SerializeField] private SpriteRenderer _switchSpriteRenderer;
                     private bool _isActive = false;
    [SerializeField] private float _switchResetDelay = 1.5f;
    [SerializeField] private TextMeshPro _switchTextCounter;
                     private int _currentClickCount = 0;
                     private Vector2 _mouseDownVector;
    [SerializeField] private float _swipeDistanceModifier = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Sprite Renderer
        if(!_switchSpriteRenderer) {
            _switchSpriteRenderer = GetComponentInChildren(typeof(SpriteRenderer), true) as SpriteRenderer;
        }
        // Set switch sprite from Scriptable Object
        _switchSpriteRenderer.sprite = _switchData.switchSprite;

        // Disable the collider on Game Over
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.enabled = true;
        InteractionManager.Instance.OnGameOver.AddListener(() => _boxCollider2D.enabled = false);
    }

    void OnMouseDown() {
        // Set the mouse position for the switch drag
        _mouseDownVector = Input.mousePosition;
    }

    void OnMouseUp() {
        // TODO: break this out for a horizontal vs vertical switch
        // Check to see if user has dragged mouse up to activate the switch
        if (Input.mousePosition.y > _mouseDownVector.y + _swipeDistanceModifier) {
            UpdateCounter();
            Activate();
        }
    }

    private void Activate() {
        if(_isActive) {
            return;
        } else _isActive = true;

        // Change the switch image
        _switchSpriteRenderer.flipY = true;
        StartCoroutine("ResetSwitch");
        // Trigger the switch activate event
        InteractionManager.Instance.OnSwitchActivate?.Invoke();
    }

    private IEnumerator ResetSwitch() {
        yield return new WaitForSeconds(_switchResetDelay);
        _switchSpriteRenderer.flipY = false;
        _isActive = false;
    }

    public void UpdateCounter()
    {
        _currentClickCount++;
        _switchTextCounter.text = _currentClickCount.ToString();
    }
}
