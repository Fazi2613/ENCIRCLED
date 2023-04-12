using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneFader_Menu : MonoBehaviour {

    public float fadeDuration = 1.0f;
    public string nextSceneName;
    public CanvasGroup fadeCanvasGroup;
    public CanvasGroup ui;
    private bool fading = false;

    public void StartGame() {
        if (!fading) {
            StartCoroutine(FadeToGame());
        }
    }

    private IEnumerator FadeToGame() {
        fading = true;
        // 0-rol 1-es ertekre erositi a halvanyulast
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration) {
            float t = elapsedTime / fadeDuration;
            fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, t);
            ui.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // A kovetkezo scene megnyitasa
        SceneManager.LoadScene(nextSceneName);
    }
}
