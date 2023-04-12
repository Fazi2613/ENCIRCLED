using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade_ToGame : MonoBehaviour {

    public float fadeDuration = 1.0f;
    public CanvasGroup fadeCanvasGroup;

    void Start() {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        // A canvas group elhalvanyitasa
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration) {
            float t = elapsedTime / fadeDuration;
            fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        fadeCanvasGroup.alpha = 0.0f;
    }
}
