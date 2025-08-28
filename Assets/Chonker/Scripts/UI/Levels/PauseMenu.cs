using System;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _optionsButton;
    private bool isPaused;

    [SerializeField] private AnimationCurve _menuMoveInCurve;
    [SerializeField] private GameObject _background;
    [SerializeField] private Transform _menuTransform;
    [SerializeField] private OptionsMenu _optionsMenu;

    [SerializeField] private Transform _pausedTargetPosition;
    [SerializeField] private Transform _notPausedTargetPosition;

    bool transitioning;

    private float transitionTime = .3f;
    private LevelManager levelManager;

    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void Start() {
        _mainMenuButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
                Time.timeScale = 1f;
            });
        });
        _resumeButton.onClick.AddListener(() => {
            CloseMenu();
            isPaused = false;
        });

        _resetButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.CurrentSceneId);
                Time.timeScale = 1f;
            });
        });

        _optionsButton.onClick.AddListener(() => {
            _optionsMenu.Activate();
        });

        _background.SetActive(false);
        _menuTransform.transform.position = _notPausedTargetPosition.position;
    }

    private void Update() {
        if (transitioning) return;
        if (levelManager.LevelFinished || PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.CurrentState == PlayerStateId.Dead) {
            if (isPaused) {
                isPaused = false;
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
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, _menuMoveInCurve,
            f => {
                _menuTransform.position =
                    Vector3.LerpUnclamped(_notPausedTargetPosition.position, _pausedTargetPosition.position, f);
            }, true,
            () => {
                transitioning = false;
                Time.timeScale = 1f;
                _optionsMenu.Deactivate();
                _background.gameObject.SetActive(false);
            }));
    }

    public void OpenMenu() {
        Time.timeScale = 0f;
        transitioning = true;
        _background.gameObject.SetActive(true);
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, _menuMoveInCurve,
            f => {
                _menuTransform.position =
                    Vector3.LerpUnclamped(_notPausedTargetPosition.position, _pausedTargetPosition.position, f);
            }, false,
            () => { transitioning = false; }));
    }
}