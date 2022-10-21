using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject continueMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Continue Menu
        if(!continueMenu) {
            continueMenu = GameObject.Find("ContinueMenu");
        }
        continueMenu.SetActive(false);

        // Events
        InteractionManager.Instance.OnGameOver.AddListener(HandleOnGameOver);
    }

    private void HandleOnGameOver() {
        continueMenu.SetActive(true);
    }
}
