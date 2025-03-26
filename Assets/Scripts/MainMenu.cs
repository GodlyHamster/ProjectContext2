using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void StartGame()
    {
        StartCoroutine(FadeOut(() => SceneManager.LoadScene("DumpingGroundsTest")));
    }

    public void QuitGame()
    {
        StartCoroutine(FadeOut(Application.Quit));
    }

    private IEnumerator FadeOut(System.Action onComplete)
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        onComplete?.Invoke();
    }
}
