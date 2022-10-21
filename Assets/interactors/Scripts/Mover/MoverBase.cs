using UnityEngine;

public class MoverBase : MonoBehaviour
{
    [Header("Mover Data Settings")]
    [SerializeField] private SO_MoverBase _moverData;
    [SerializeField] private SpriteRenderer _moverSpriteRenderer;
    // [SerializeField] private Animator _moverAnimator;
                     private bool _isRotatingClockwise = true;
                     private bool _isRotating = false;
    [SerializeField] private float _moverRotationSpeedModifier = 1.0f;
                     private float _rotationDirectionModifier = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Sprite Renderer
        if(!_moverSpriteRenderer) {
            _moverSpriteRenderer = GetComponentInChildren(typeof(SpriteRenderer), true) as SpriteRenderer;
        }
        _moverSpriteRenderer.sprite = _moverData.moverSprite;
        // Animator
        // if(!_moverAnimator) {
        //     _moverAnimator = GetComponent<Animator>();
        // }
        // _moverAnimator.speed = 0;

        InteractionManager.Instance.OnButtonClick.AddListener(HandleRotationActive);
        InteractionManager.Instance.OnSwitchActivate.AddListener(HandleRotationDirection);
    }

    void Update() {
        // if(_isRotating) {
        //     transform.Rotate(0.0f, 0.0f, _rotationDirectionModifier * _moverRotationSpeedModifier * Time.deltaTime);
        // }
        if(_isRotating) {
            transform.Rotate(0.0f, 0.0f, _rotationDirectionModifier * _moverRotationSpeedModifier);
        }
    }

    private void HandleRotationActive() {
        if(_isRotating) {
            // _moverAnimator.speed = 0;
            _isRotating = false;
        } else {
            // _moverAnimator.speed = 1 * _moverRotationSpeedModifier;
            _isRotating = true;
        }
    }

// TODO: Fix issue where changing directions while speed is 0 causes an animation 'burp'
    private void HandleRotationDirection() {
        if(!_isRotatingClockwise) {
            _isRotatingClockwise = true;
            _rotationDirectionModifier = -1.0f;
            // _moverAnimator.SetBool("isRotatingClockwise", true);
        } else {
            _isRotatingClockwise = false;
            _rotationDirectionModifier = 1.0f;
            // _moverAnimator.SetBool("isRotatingClockwise", false);
        }
    }
}
