using System;
using Chonker.Core.Tween;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button ResetButton;
    [SerializeField] private Button OptionsButton;
    private bool isPaused;

    [SerializeField] private AnimationCurve MenuMoveInCurve;
    [SerializeField] private GameObject Background;
    [SerializeField] private Transform MenuTransform;
    [SerializeField] private GameObject OptionsMenu;

    [SerializeField] private Transform PausedTargetPosition;
    [SerializeField] private Transform NotPausedTargetPosition;

    bool transitioning;

    private float transitionTime = .3f;
    private LevelManager levelManager;

    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void Start() {
        MainMenuButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
                Time.timeScale = 1f;
            });
        });
        ResumeButton.onClick.AddListener(() => {
            CloseMenu();
            isPaused = false;
        });

        ResetButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.CurrentSceneId);
                Time.timeScale = 1f;
            });
        });

        OptionsButton.onClick.AddListener(() => { OptionsMenu.SetActive(true); });

        Background.SetActive(false);
        MenuTransform.transform.position = NotPausedTargetPosition.position;
    }

    private void Update() {
        if (transitioning) return;
        if (levelManager.LevelFinished) {
            if (isPaused) {
                CloseMenu();
            }
            return;
        }
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            if (isPaused) {
                CloseMenu();
            }
            else {
                OpenMenu();
            }

            isPaused = !isPaused;
        }
    }

    public void CloseMenu() {
        transitioning = true;
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, MenuMoveInCurve,
            f => {
                MenuTransform.position =
                    Vector3.LerpUnclamped(NotPausedTargetPosition.position, PausedTargetPosition.position, f);
            }, true,
            () => {
                transitioning = false;
                Time.timeScale = 1f;
                OptionsMenu.SetActive(false);
                Background.gameObject.SetActive(false);
            }));
    }

    public void OpenMenu() {
        Time.timeScale = 0f;
        transitioning = true;
        Background.gameObject.SetActive(true);
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, MenuMoveInCurve,
            f => {
                MenuTransform.position =
                    Vector3.LerpUnclamped(NotPausedTargetPosition.position, PausedTargetPosition.position, f);
            }, false,
            () => { transitioning = false; }));
    }
}