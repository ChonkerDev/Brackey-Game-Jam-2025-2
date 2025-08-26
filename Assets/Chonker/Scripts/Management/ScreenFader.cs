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
    }

    public static void FadeOut(float duration, Action onComplete = null, EaseType easeType = EaseType.Linear) {
        instance.StartCoroutine(TweenCoroutines.RunTaper(duration,  null, onComplete, easeType));
    }
    
    public static void FadeIn(float duration, Action onComplete = null, EaseType easeType = EaseType.Linear) {
        instance.StartCoroutine(TweenCoroutines.RunTaper(duration,  null, onComplete, easeType));
    }
    
    
}
