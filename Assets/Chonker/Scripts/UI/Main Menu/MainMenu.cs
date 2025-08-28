using System;
using System.Collections;
using Chonker.Core;
using Chonker.Core.Attributes;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : NavigationUIMenu
{
    [SerializeField, PrefabModeOnly] private CanvasGroup PressAnyKeyCanvasGroup;
    [SerializeField] private AnimationCurve MainMenuPopInScaleCurve;

    [SerializeField, PrefabModeOnly] private Button NewGameButton;
    [SerializeField, PrefabModeOnly] private Button ContinueButton;
    [SerializeField, PrefabModeOnly] private Button LevelSelectButton;
    [SerializeField, PrefabModeOnly] private Button SettingsButton;

    [SerializeField, PrefabModeOnly] private LevelSelectionMenu LevelSelectionMenu;

    [SerializeField, PrefabModeOnly]
    private AudioSource _audioSource;

    [SerializeField, PrefabModeOnly] private AudioClip _startPressedSoundClip;

    protected override void OnAwake() {
        if (PersistantDataManager.instance.GetCampaignProgress() == SceneManagerWrapper.SceneId.Level1) {
            ContinueButton.gameObject.SetActive(false);
            Navigation newGameButtonNav = NewGameButton.navigation;
            newGameButtonNav.selectOnDown = LevelSelectButton;
            NewGameButton.navigation = newGameButtonNav;
            
            Navigation levelSectionButtonNav = LevelSelectButton.navigation;
            levelSectionButtonNav.selectOnUp = NewGameButton;
            LevelSelectButton.navigation = levelSectionButtonNav;
        }
    }

    void Start() {
        Time.timeScale = 1f;
        ScreenFader.TurnOff();
        NewGameButton.onClick.AddListener(() => {
            GameManager.instance.CurrentGameMode = GameManager.GameMode.Campaign;
            ScreenFader.FadeOut(2, () => SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.CampaignIntro),
                EaseType.EaseInQuad);
        });
        ContinueButton.onClick.AddListener(() => {
            GameManager.instance.CurrentGameMode = GameManager.GameMode.Campaign;
            ScreenFader.FadeOut(2,
                () => SceneManagerWrapper.LoadScene(PersistantDataManager.instance.GetCampaignProgress()),
                EaseType.EaseInQuad);
        });
        LevelSelectButton.onClick.AddListener(() => {
            GameManager.instance.CurrentGameMode = GameManager.GameMode.TimeTrial;
            LevelSelectionMenu.gameObject.SetActive(true);
        });
        SettingsButton.onClick.AddListener(() => { });
        StartCoroutine(ProcessPressAnyKeyUI());
    }

    private IEnumerator ProcessPressAnyKeyUI() {
        RectTransform mainMenuRectTransform = GetComponent<RectTransform>();
        mainMenuRectTransform.localScale = Vector3.zero;
        Deactivate();

        while ((Keyboard.current == null || !Keyboard.current.anyKey.wasPressedThisFrame) &&
               (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)) {
            yield return null;
        }
        _audioSource.PlayOneShot(_startPressedSoundClip);

        Activate();
        yield return StartCoroutine(TweenCoroutines.RunTaper(.5f,
            f => { PressAnyKeyCanvasGroup.alpha = 1 - f; },
            () => { PressAnyKeyCanvasGroup.gameObject.SetActive(false); }));
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaper(.3f, MainMenuPopInScaleCurve,
            curveAlpha => { mainMenuRectTransform.localScale = Vector3.one * curveAlpha; }));
    }
}