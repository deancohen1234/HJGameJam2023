using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator focusAnimator;

    public CanvasGroup fader;
    public float fadeDuration = 1.0f;

    public GameObject gameOverScreen;
    public GameObject winGameScreen;
    public CanvasGroup startGameScreen;

    public CanvasGroup inputBlocker;

    private void Awake() {
        instance = this;

        gameOverScreen.SetActive(false);

        inputBlocker.blocksRaycasts = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }
    }

    public void StartGame() {

        focusAnimator.SetTrigger("Reset");

        startGameScreen.DOFade(0, 0.75f);

        inputBlocker.blocksRaycasts = false;
    }

    public void EndGame() {

        fader.DOFade(1.0f, fadeDuration).SetEase(Ease.Linear).OnComplete(FadeToBlackEndGameComplete);
    }

    public void WinGame() {
        fader.DOFade(1.0f, fadeDuration).SetEase(Ease.Linear).OnComplete(FadeToBlackEndGameComplete);
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void FadeToBlackEndGameComplete() {
        gameOverScreen.SetActive(true);
    }
}
