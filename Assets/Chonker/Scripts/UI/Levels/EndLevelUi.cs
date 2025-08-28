using System;
using System.Collections;
using Chonker.Core.Attributes;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelUi : MonoBehaviour
{
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

    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    IEnumerator Start() {
        _menuTransform.transform.position = _hiddenPosition.position;
        _newRecordText.gameObject.SetActive(false);
        if (GameManager.instance.CurrentGameMode == GameManager.GameMode.Campaign) {
            _tryAgainButton.gameObject.SetActive(false);
        } else if (GameManager.instance.CurrentGameMode == GameManager.GameMode.TimeTrial) {
            _nextLevelButton.gameObject.SetActive(false);
        }
        _nextLevelButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(levelManager.NextScene);
            });
        });
        _tryAgainButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.CurrentSceneId);
            });
        });
        _mainMenuButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
            });
        });
        
        while (!levelManager.LevelFinished) {
            yield return null;
        }

        TimeSpan t = TimeSpan.FromSeconds(levelManager.TimeTaken);

        if (GameManager.instance.CurrentGameMode == GameManager.GameMode.TimeTrial) {
            float storedTime = PersistantDataManager.instance.GetLevelTime(SceneManagerWrapper.CurrentSceneId);
            if (storedTime > levelManager.TimeTaken) {
                PersistantDataManager.instance.SetLevelTime(SceneManagerWrapper.CurrentSceneId, storedTime);
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