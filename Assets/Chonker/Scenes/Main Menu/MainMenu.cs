using System.Collections;
using Chonker.Core.Tween;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup PressAnyKeyCanvasGroup;
    [SerializeField] private CanvasGroup MainMenuCanvasGroup;

    [SerializeField] private AnimationCurve MainMenuPopInScaleCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start() {
        RectTransform mainMenuRectTransform = MainMenuCanvasGroup.GetComponent<RectTransform>();
        mainMenuRectTransform.localScale = Vector3.zero;
        MainMenuCanvasGroup.gameObject.SetActive(false);

        while (!Keyboard.current.anyKey.wasPressedThisFrame) {
            yield return null;
        }


        MainMenuCanvasGroup.gameObject.SetActive(true);
        yield return StartCoroutine(TweenCoroutines.RunTaper(.5f,
            f => { PressAnyKeyCanvasGroup.alpha = 1 - f; },
            () => { PressAnyKeyCanvasGroup.gameObject.SetActive(false); }));
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaper(.5f, MainMenuPopInScaleCurve,
            curveAlpha => { mainMenuRectTransform.localScale = Vector3.one * curveAlpha; }));
    }
}