using System;
using System.Collections;
using Chonker.Core.Attributes;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelUi : NavigationUIMenu
{
    [SerializeField, PrefabModeOnly] private AudioSource _generalSFXAudioSource;
    [SerializeField, PrefabModeOnly] private AudioClip _finshedLevelAudioClip;
    [SerializeField] private AnimationCurve _menuMoveInCurve;
    [SerializeField, PrefabModeOnly] private Transform _menuTransform;
    [SerializeField, PrefabModeOnly] private Transform _viewablePosition;
    [SerializeField, PrefabModeOnly] private Transform _hiddenPosition;
    private LevelManager levelManager;
    private float transitionTime = .5f;
    [SerializeField, PrefabModeOnly] private TextMeshProUGUI _timeTakenText;
    [SerializeField, PrefabModeOnly] private Button _nextLevelButton;
    [SerializeField, PrefabModeOnly] private Button _mainMenuButton;
    [SerializeField, PrefabModeOnly] private Button _tryAgainButton;
    [SerializeField, PrefabModeOnly] private TextMeshProUGUI _newRecordText;

    protected override void OnAwake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    IEnumerator Start() {
        Deactivate();
        _menuTransform.transform.position = _hiddenPosition.position;
        _newRecordText.gameObject.SetActive(false);
        Navigation mainMenuButtonNav = _mainMenuButton.navigation;
        if (GameManager.instance.CurrentGameMode == GameManager.GameMode.Campaign) {
            _tryAgainButton.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(true);
            defaultSelectable = _nextLevelButton;
            mainMenuButtonNav.selectOnLeft = _nextLevelButton;
            mainMenuButtonNav.selectOnRight = _nextLevelButton;
        } else if (GameManager.instance.CurrentGameMode == GameManager.GameMode.TimeTrial) {
            _nextLevelButton.gameObject.SetActive(false);
            _tryAgainButton.gameObject.SetActive(true);
            defaultSelectable = _tryAgainButton;
            mainMenuButtonNav.selectOnLeft = _tryAgainButton;
            mainMenuButtonNav.selectOnRight = _tryAgainButton;
        }

        _mainMenuButton.navigation = mainMenuButtonNav;
        _nextLevelButton.onClick.AddListener(() => {
            ClearCurrentInteractable();
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(levelManager.NextScene);
            });
        });
        _tryAgainButton.onClick.AddListener(() => {
            ClearCurrentInteractable();
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.CurrentSceneId);
            });
        });
        _mainMenuButton.onClick.AddListener(() => {
            ClearCurrentInteractable();
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
            });
        });
        
        while (!levelManager.LevelFinished) {
            yield return null;
        }

        yield return null; // eat a frame
        Activate();
        _generalSFXAudioSource.PlayOneShot(_finshedLevelAudioClip);
        TimeSpan t = TimeSpan.FromSeconds(levelManager.TimeTaken);

        if (GameManager.instance.CurrentGameMode == GameManager.GameMode.TimeTrial) {
            float storedTime = PersistantDataManager.instance.GetLevelTime(SceneManagerWrapper.CurrentSceneId);
            if (storedTime > levelManager.TimeTaken) {
                PersistantDataManager.instance.SetLevelTime(SceneManagerWrapper.CurrentSceneId, levelManager.TimeTaken);
                _newRecordText.gameObject.SetActive(true);
            }
        }
        string formatted = string.Format("{0:D2}:{1:D2}.{2:D3}",
            t.Minutes,
            t.Seconds,
            t.Milliseconds);
        _timeTakenText.text = "Time Taken" +
                              "\n" + formatted;
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, _menuMoveInCurve,
            f => {
                _menuTransform.position =
                    Vector3.LerpUnclamped(_hiddenPosition.position, _viewablePosition.position, f);
            }));
        
    }
}