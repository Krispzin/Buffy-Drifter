using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;  // Reference to the UI Image component used for fading

    public float fadeSpeed = 1f;  // Speed at which the fade occurs

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(1f);
        fadeImage.CrossFadeAlpha(0f, fadeSpeed, false);
        yield return new WaitForSeconds(fadeSpeed);
        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(0f);
        fadeImage.CrossFadeAlpha(1f, fadeSpeed, false);
        yield return new WaitForSeconds(fadeSpeed);
        SceneManager.LoadScene(sceneName);
    }
}