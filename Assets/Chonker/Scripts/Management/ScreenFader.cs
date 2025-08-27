using System;
using Chonker.Core;
using Chonker.Core.Tween;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private static ScreenFader instance;
    private static GameObject canvasGameObject => instance.canvasGroup.gameObject;

    private void Awake() {
        if (!instance) {
            instance = this;
            TurnOff();
        }
    }

    public static void FadeOut(float duration, Action onComplete = null, EaseType easeType = EaseType.Linear) {
        canvasGameObject.SetActive(true);
        instance.StartCoroutine(TweenCoroutines.RunTaperRealTime(duration, f => { instance.canvasGroup.alpha = f; },
            () => { onComplete?.Invoke(); }, easeType));
    }

    public static void FadeIn(float duration, Action onComplete = null, EaseType easeType = EaseType.EaseInQuad) {
        canvasGameObject.SetActive(true);
        instance.StartCoroutine(TweenCoroutines.RunTaperRealTime(duration, 
            f => { instance.canvasGroup.alpha = 1 - f; },
            () => { canvasGameObject.SetActive(false); },
            easeType));
    }

    public static void TurnOn() {
        canvasGameObject.SetActive(true);
        instance.canvasGroup.alpha = 1;
    }

    public static void TurnOff() {
        canvasGameObject.SetActive(false);
        instance.canvasGroup.alpha = 0;
    }
}