using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _continueMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Continue Menu
        if(!_continueMenu) {
            _continueMenu = GameObject.Find("ContinueMenu");
        }
        _continueMenu.SetActive(false);

        // Events
        InteractionManager.Instance.OnGameOver.AddListener(HandleOnGameOver);
    }

    private void HandleOnGameOver() {
        _continueMenu.SetActive(true);
    }
}
