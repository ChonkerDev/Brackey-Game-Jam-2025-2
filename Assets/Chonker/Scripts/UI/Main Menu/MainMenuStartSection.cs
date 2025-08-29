using System.Collections;
using Chonker.Core.Tween;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuStartSection : NavigationUIMenu
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private AudioSource _audioSource;

    IEnumerator Start() {
        Time.timeScale = 1f;
        yield return null;
        Activate();
        while (Keyboard.current == null || !Keyboard.current.anyKey.wasPressedThisFrame) {
            yield return null;
        }

        _audioSource.Play();
        StartCoroutine(TweenCoroutines.RunTaper(.5f,
            f => { canvasGroup.alpha = 1 - f; },
            () => {
                mainMenu.Activate();
                Deactivate();
                StartCoroutine(TweenCoroutines.RunAnimationCurveTaper(.3f, mainMenu.MainMenuPopInScaleCurve,
                    curveAlpha => { mainMenu.RectTransform.localScale = Vector3.one * curveAlpha; } ));
            }));

    }
}