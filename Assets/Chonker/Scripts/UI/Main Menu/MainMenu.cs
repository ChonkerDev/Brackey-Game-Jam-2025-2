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
    [SerializeField] private CanvasGroup PressAnyKeyCanvasGroup;
    public AnimationCurve MainMenuPopInScaleCurve;

    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button LevelSelectButton;
    [SerializeField] private Button SettingsButton;

    [SerializeField] private LevelSelectionMenu LevelSelectionMenu;
    

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
        Deactivate();
        ScreenFader.TurnOff();
        NewGameButton.onClick.AddListener(() => {
            ClearCurrentInteractable();
            GameManager.instance.CurrentGameMode = GameManager.GameMode.Campaign;
            ScreenFader.FadeOut(2, () => SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.CampaignIntro),
                EaseType.EaseInQuad);
        });
        ContinueButton.onClick.AddListener(() => {
            ClearCurrentInteractable();
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
    }
}