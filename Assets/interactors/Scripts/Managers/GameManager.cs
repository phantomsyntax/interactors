using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame() {
        SceneManager.LoadScene("00_mainScene");
    }

    public void QuitGame() {
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
