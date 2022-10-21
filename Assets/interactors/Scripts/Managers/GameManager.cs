using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreen;

    public void RestartGame() {
        SceneManager.LoadScene("00_mainScene");
    }

    public void QuitGame() {
        // Black Screen for end game
        if(!_blackScreen) {
            _blackScreen = GameObject.Find("BlackScreen");
        }
        _blackScreen.SetActive(true);
        StartCoroutine("ExitGameDelayed");
    }

    public IEnumerator ExitGameDelayed() {
        yield return new WaitForSeconds(1.0f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
