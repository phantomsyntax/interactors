using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    // Events
    public UnityEvent OnButtonClick;
    public UnityEvent OnGameOver;
    public UnityEvent OnSwitchActivate;
    // Counter
    private int totalClickCount = 0;
    private bool isGameOver = false;
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
        if(!isGameOver && totalClickCount == 10) {
            isGameOver = true;
            HandleOnGameOver();
        }
    }

    private void HandleOnButtonClick() {
        totalClickCount++;
    }

    private void HandleOnSwitchActivate() {
        totalClickCount++;
    }

    private void HandleOnGameOver() {
        OnGameOver?.Invoke();
        Debug.Log("the game is over");
        // Throw UI screen
        // Disable Interactive
    }
}
