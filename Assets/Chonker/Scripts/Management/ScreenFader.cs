using System;
using Chonker.Core;
using Chonker.Core.Tween;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private static ScreenFader instance;

    private void Awake() {
        instance = this;
        TurnOff();
    }

    public static void FadeOut(float duration, Action onComplete = null, EaseType easeType = EaseType.Linear) {
        instance.gameObject.SetActive(true);
        instance.StartCoroutine(TweenCoroutines.RunTaper(duration, f => { instance.canvasGroup.alpha = f; }, 
            () => {
                onComplete?.Invoke();
            }, easeType));
    }

    public static void FadeIn(float duration, Action onComplete = null, EaseType easeType = EaseType.Linear) {
        instance.gameObject.SetActive(true);
        instance.StartCoroutine(TweenCoroutines.RunTaper(duration, f => {
                instance.gameObject.SetActive(false);
                instance.canvasGroup.alpha = 1 - f;
            },
            onComplete, easeType));
    }

    public static void TurnOn() {
        instance.gameObject.SetActive(true);
        instance.canvasGroup.alpha = 1;
    }

    public static void TurnOff() {
        instance.gameObject.SetActive(false);
        instance.canvasGroup.alpha = 0;
    }
}