using System.Collections;
using Chonker.Core.Tween;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup PressAnyKeyCanvasGroup;
    [SerializeField] private CanvasGroup MainMenuCanvasGroup;

    [SerializeField] private AnimationCurve MainMenuPopInScaleCurve;

    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button SettingsButton;

    void Start() {
        NewGameButton.onClick.AddListener(() => {
            
        });
        ContinueButton.onClick.AddListener(() => {
            
        });
        SettingsButton.onClick.AddListener(() => {
            
        });
        StartCoroutine(ProcessPressAnyKeyUI());
    }

    private IEnumerator ProcessPressAnyKeyUI() {
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
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaper(.3f, MainMenuPopInScaleCurve,
            curveAlpha => { mainMenuRectTransform.localScale = Vector3.one * curveAlpha; }));
    }
}