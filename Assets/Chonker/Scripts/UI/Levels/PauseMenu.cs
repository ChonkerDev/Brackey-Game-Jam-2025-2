using System;
using Chonker.Core.Attributes;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : NavigationUIMenu
{
    [SerializeField, PrefabModeOnly] private AudioSource _generalSFXAudioSource;
    [SerializeField, PrefabModeOnly] private AudioClip _pauseMenuOpenAudioClip;
    [SerializeField, PrefabModeOnly] private AudioClip _pauseMenuClosedAudioClip;
    [SerializeField, PrefabModeOnly] private Button _mainMenuButton;
    [SerializeField, PrefabModeOnly] private Button _resumeButton;
    [SerializeField, PrefabModeOnly] private Button _resetButton;
    [SerializeField, PrefabModeOnly] private Button _optionsButton;
    public bool IsPaused { get; private set; }

    [SerializeField] private AnimationCurve _menuMoveInCurve;
    [SerializeField, PrefabModeOnly] private GameObject _background;
    [SerializeField, PrefabModeOnly] private Transform _menuTransform;
    [SerializeField, PrefabModeOnly] private OptionsMenu _optionsMenu;

    [SerializeField, PrefabModeOnly] private Transform _pausedTargetPosition;
    [SerializeField, PrefabModeOnly] private Transform _notPausedTargetPosition;

    bool transitioning;

    private float transitionTime = .3f;
    private LevelManager levelManager;

    public static PauseMenu instance;

    protected override void OnAwake() {
        levelManager = FindAnyObjectByType<LevelManager>();
        instance = this;
    }

    void Start() {
        Deactivate();
        _mainMenuButton.onClick.AddListener(() => {
            ClearCurrentInteractable();
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
                Time.timeScale = 1f;
            });
        });
        _resumeButton.onClick.AddListener(() => { CloseMenu(); });

        _resetButton.onClick.AddListener(() => {
            ClearCurrentInteractable();
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.CurrentSceneId);
                Time.timeScale = 1f;
            });
        });

        _optionsButton.onClick.AddListener(() => { _optionsMenu.Activate(); });

        _menuTransform.transform.position = _notPausedTargetPosition.position;
    }

    protected override void processCurrentMenu() {
        if (transitioning) return;
        if (PlayerInputWrapper.instance.wasExitMenuPressedThisFrame()) {
            CloseMenu();
        }
    }

    protected override void OnUpdate() {
        if (transitioning) return;
        if (levelManager.LevelFinished ||
            PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.CurrentState == PlayerStateId.Dead) {
            if (IsPaused) {
                CloseMenu();
            }

            return;
        }

        if (PlayerInputWrapper.instance.wasExitMenuPressedThisFrame()) {
            if (!IsPaused) {
                OpenMenu();
            }
        }
    }

    public void CloseMenu() {
        transitioning = true;
        _generalSFXAudioSource.PlayOneShot(_pauseMenuClosedAudioClip);
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, _menuMoveInCurve,
            f => {
                _menuTransform.position =
                    Vector3.LerpUnclamped(_notPausedTargetPosition.position, _pausedTargetPosition.position, f);
            }, true,
            () => {
                transitioning = false;
                Time.timeScale = 1f;
                _optionsMenu.Deactivate();
                Deactivate();
                IsPaused = false;
            }));
    }

    public void OpenMenu() {
        Time.timeScale = 0f;
        transitioning = true;
        IsPaused = true;
        Activate();
        _generalSFXAudioSource.PlayOneShot(_pauseMenuOpenAudioClip);
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, _menuMoveInCurve,
            f => {
                _menuTransform.position =
                    Vector3.LerpUnclamped(_notPausedTargetPosition.position, _pausedTargetPosition.position, f);
            }, false,
            () => { transitioning = false; }));
    }
}