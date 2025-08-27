using System;
using System.Collections;
using Chonker.Core.Tween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelUi : MonoBehaviour
{
    [SerializeField] private AnimationCurve MenuMoveInCurve;

    [SerializeField] private Transform MenuTransform;
    [SerializeField] private Transform StartPosition;
    [SerializeField] private Transform EndPosition;
    private LevelManager levelManager;
    private float transitionTime = .5f;
    [SerializeField] private TextMeshProUGUI _timeTakenText;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _mainMenuButton;

    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    IEnumerator Start() {
        _nextLevelButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(levelManager.NextScene);
            });
        });
        _mainMenuButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => {
                SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
            });
        });

        MenuTransform.gameObject.SetActive(false);
        while (!levelManager.LevelFinished) {
            yield return null;
        }

        MenuTransform.gameObject.SetActive(true);

        TimeSpan t = TimeSpan.FromMilliseconds(levelManager.TimeTaken);

        string formatted = string.Format("{0:D2}:{1:D2}.{2:D3}",
            t.Minutes,
            t.Seconds,
            t.Milliseconds);
        _timeTakenText.text = "Time Taken" +
                              "\n" + formatted;
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(transitionTime, MenuMoveInCurve,
            f => {
                MenuTransform.position =
                    Vector3.LerpUnclamped(StartPosition.position, EndPosition.position, f);
            }));
        
    }
}