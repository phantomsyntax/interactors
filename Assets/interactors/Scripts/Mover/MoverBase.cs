using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBase : MonoBehaviour
{
    [Header("Mover Data Settings")]
    [SerializeField] private SO_MoverBase moverData;
    [SerializeField] private SpriteRenderer moverSpriteRenderer;
    [SerializeField] private Animator moverAnimator;
                     private bool isAnimatingClockwise = true;
                     private bool isRotating = false;
    [SerializeField] private float moverRotationSpeedModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Sprite Renderer
        if(!moverSpriteRenderer) {
            moverSpriteRenderer = GetComponentInChildren(typeof(SpriteRenderer), true) as SpriteRenderer;
        }
        moverSpriteRenderer.sprite = moverData.moverSprite;
        // Animator
        if(!moverAnimator) {
            moverAnimator = GetComponent<Animator>();
        }
        moverAnimator.speed = 0;

        InteractionManager.Instance.OnButtonClick.AddListener(HandleRotationActive);
        InteractionManager.Instance.OnSwitchActivate.AddListener(HandleRotationDirection);
    }

    private void HandleRotationActive() {
        if(isRotating) {
            moverAnimator.speed = 0;
            isRotating = false;
        } else {
            moverAnimator.speed = 1 * moverRotationSpeedModifier;
            isRotating = true;
        }
    }

    private void HandleRotationDirection() {
        if(!isAnimatingClockwise) {
            isAnimatingClockwise = true;
            moverAnimator.SetBool("isRotatingClockwise", true);
        } else {
            isAnimatingClockwise = false;
            moverAnimator.SetBool("isRotatingClockwise", false);
        }
    }
}
