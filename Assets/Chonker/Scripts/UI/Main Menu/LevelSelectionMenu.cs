using System;
using System.Collections.Generic;
using Chonker.Core;
using Chonker.Core.Attributes;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectionMenu : NavigationUIMenu
{
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private HorizontalLayoutGroup CardContainer;
    [SerializeField] private LevelSelectCard CardTemplate;
    [SerializeField] private Button _playButton;
    [SerializeField] private AudioSource _cycleButtonsAudioSource;
    [SerializeField] private AnimationCurve shiftTransformCurve;

    [SerializeField] private LevelSelectCardData[] CardDatas;
    private List<LevelSelectCard> cardInstances = new();
    private float cardTotalSpacing;
    private int currentCardIndex = 0;

    private void Start() {
        cardTotalSpacing = CardTemplate.GetComponent<RectTransform>().sizeDelta.x + CardContainer.spacing;
        foreach (var levelSelectCardData in CardDatas) {
            LevelSelectCard card = Instantiate(CardTemplate, CardContainer.transform);
            card.SetData(levelSelectCardData);
            cardInstances.Add(card);
        }

        _playButton.onClick.AddListener(() => {
            ScreenFader.FadeOut(.5f, () => { SceneManagerWrapper.LoadScene(cardInstances[currentCardIndex].LevelId); },
                EaseType.EaseInQuad);
        });
        
        ExitButton.onClick.AddListener(Deactivate);

        LeftButton.gameObject.SetActive(false);
        Destroy(CardTemplate.gameObject);
        Deactivate();
    }

    public void ShiftRight() {
        LeftButton.gameObject.SetActive(true);
        currentCardIndex++;
        if (currentCardIndex > CardDatas.Length - 1) {
            currentCardIndex = CardDatas.Length - 1;
            return;
        }

        if (currentCardIndex == CardDatas.Length - 1) {
            RightButton.gameObject.SetActive(false);
            EventSystem.current?.SetSelectedGameObject(LeftButton.gameObject);
        }

        StopAllCoroutines();
        ShiftCards();
    }

    public void ShiftLeft() {
        RightButton.gameObject.SetActive(true);
        currentCardIndex--;
        if (currentCardIndex < 0) {
            currentCardIndex = 0;
            return;
        }

        if (currentCardIndex == 0) {
            LeftButton.gameObject.SetActive(false);
            EventSystem.current?.SetSelectedGameObject(RightButton.gameObject);
        }

        StopAllCoroutines();
        ShiftCards();
    }

    private void ShiftCards() {
        _cycleButtonsAudioSource.Play();
        Vector3 startPosition = CardContainer.transform.localPosition;
        Vector3 endPosition = Vector3.left * cardTotalSpacing * currentCardIndex;
        StartCoroutine(
            TweenCoroutines.RunAnimationCurveTaperRealTime(.7f, shiftTransformCurve,
                f => { CardContainer.transform.localPosition = Vector3.LerpUnclamped(startPosition, endPosition, f); },
                false,
                () => { CardContainer.transform.localPosition = endPosition; })
        );
    }
}