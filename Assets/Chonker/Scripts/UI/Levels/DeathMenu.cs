using System;
using System.Collections;
using Chonker.Core.Attributes;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : NavigationUIMenu
{
    [SerializeField] private AnimationCurve _menuMoveInCurve;
    [SerializeField, PrefabModeOnly] private AudioSource _generalSFXAudioSource;
    [SerializeField, PrefabModeOnly] private AudioClip _deadSoundEffectAudioClip;

    [SerializeField, PrefabModeOnly] private RectTransform _menuTransform;
    [SerializeField, PrefabModeOnly] private RectTransform _hiddenPosition;
    [SerializeField, PrefabModeOnly] private RectTransform _viewablePosition;
    [SerializeField, PrefabModeOnly] private Button _restartButton;
    [SerializeField, PrefabModeOnly] private Button _mainMenuButton;
    private LevelManager levelManager;
    private float transitionTime = .5f;

    protected override void OnAwake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    IEnumerator Start() {
        Deactivate();
        _restartButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.CurrentSceneId);
                Time.timeScale = 1f;
            });
        });
        _mainMenuButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
                Time.timeScale = 1f;
            });
        });
        _menuTransform.position = _hiddenPosition.position;
        while (PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.CurrentState != PlayerStateId.Dead) {
            yield return null;
        }

        _generalSFXAudioSource.PlayOneShot(_deadSoundEffectAudioClip);
        DeathTracker.instance.DeathTransforms.Add(new DeathTransform(
            PlayerRaccoonComponentContainer.PlayerInstance.transform.position,
            PlayerRaccoonComponentContainer.PlayerInstance.PlayerMovementInputWrapper.transform.eulerAngles.z));

        Time.timeScale = 0;
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, _menuMoveInCurve,
            f => {
                _menuTransform.position =
                    Vector3.LerpUnclamped(_hiddenPosition.position, _viewablePosition.position, f);
            }));
    }
}