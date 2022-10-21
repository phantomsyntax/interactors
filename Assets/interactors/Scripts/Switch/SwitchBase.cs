using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchBase : MonoBehaviour, ICountable
{
                     private BoxCollider2D boxCollider2D;
    [Header("Switch Data Settings")]
    [SerializeField] private SO_SwitchBase switchData;
    [SerializeField] private SpriteRenderer switchSpriteRenderer;
                     private bool isActive = false;
    [SerializeField] private int switchResetDelay = 2;
    [SerializeField] private TextMeshPro switchTextCounter;
                     private int currentClickCount = 0;
                     private Vector2 mouseDownVector;
    [SerializeField] private float swipeDistanceModifier = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Sprite Renderer
        if(!switchSpriteRenderer) {
            switchSpriteRenderer = GetComponentInChildren(typeof(SpriteRenderer), true) as SpriteRenderer;
        }
        // Set switch sprite from Scriptable Object
        switchSpriteRenderer.sprite = switchData.switchSprite;

        // Disable the collider on Game Over
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = true;
        InteractionManager.Instance.OnGameOver.AddListener(() => boxCollider2D.enabled = false);
    }

    void OnMouseDown() {
        // Set the mouse position for the switch drag
        mouseDownVector = Input.mousePosition;
    }

    void OnMouseUp() {
        // TODO: break this out for a horizontal vs vertical switch
        // Check to see if user has dragged mouse up to activate the switch
        if (Input.mousePosition.y > mouseDownVector.y + swipeDistanceModifier) {
            UpdateCounter();
            Activate();
        }
    }

    private void Activate() {
        if(isActive) {
            return;
        } else isActive = true;

        // Change the switch image
        switchSpriteRenderer.flipY = true;
        StartCoroutine("ResetSwitch");
        // Trigger the switch activate event
        InteractionManager.Instance.OnSwitchActivate?.Invoke();
    }

    private IEnumerator ResetSwitch() {
        yield return new WaitForSeconds(switchResetDelay);
        switchSpriteRenderer.flipY = false;
        isActive = false;
    }

    public void UpdateCounter()
    {
        currentClickCount++;
        switchTextCounter.text = currentClickCount.ToString();
    }
}
