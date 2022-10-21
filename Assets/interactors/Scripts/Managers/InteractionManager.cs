using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    // Events
    public UnityEvent OnButtonClick;
    public UnityEvent OnGameOver;
    public UnityEvent OnSwitchActivate;
    // Counter
    private int _totalClickCount = 0;
    private bool _isGameOver = false;
    // Singleton Instance
    public static InteractionManager Instance;

    void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnButtonClick.AddListener(HandleOnButtonClick);
        OnSwitchActivate.AddListener(HandleOnSwitchActivate);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isGameOver && _totalClickCount == 10) {
            _isGameOver = true;
            HandleOnGameOver();
        }
    }

    private void HandleOnButtonClick() {
        _totalClickCount++;
    }

    private void HandleOnSwitchActivate() {
        _totalClickCount++;
    }

    private void HandleOnGameOver() {
        _totalClickCount = 0;
        _isGameOver = false;
        OnGameOver?.Invoke();
    }
}
