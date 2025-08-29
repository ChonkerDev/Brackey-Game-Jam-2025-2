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
    [SerializeField] private Image _leftArrow;
    private Vector2 leftArrowDefaultPosition;
    [SerializeField] private Image _rightArrow;
    [SerializeField] private Color arrowDefaultColor;
    private Vector2 rightArrowDefaultPosition;
    private float arrowBumpAmplitudeModifier = .2f;
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
            ClearCurrentInteractable();
            ScreenFader.FadeOut(.5f, () => { SceneManagerWrapper.LoadScene(cardInstances[currentCardIndex].LevelId); },
                EaseType.EaseInQuad);
        });
        arrowDefaultColor = _leftArrow.color;
        leftArrowDefaultPosition = _leftArrow.rectTransform.position;
        rightArrowDefaultPosition = _rightArrow.rectTransform.position;
        ExitButton.onClick.AddListener(Deactivate);

        _leftArrow.color = Color.clear;
        Destroy(CardTemplate.gameObject);
        Deactivate();
    }

    public override void Activate() {
        base.Activate();
        Vector3 endPosition = Vector3.left * (cardTotalSpacing * currentCardIndex);
        CardContainer.transform.localPosition = endPosition;
    }

    protected override void processCurrentMenu() {
        if (PlayerInputWrapper.instance.wasNavigateLeftPressedThisFrame()) {
            ShiftLeft();
        }

        if (PlayerInputWrapper.instance.wasNavigateRightPressedThisFrame()) {
            ShiftRight();
        }

        if (PlayerInputWrapper.instance.wasExitMenuPressedThisFrame()) {
            Deactivate();
        }
    }

    public void ShiftRight() {
        _leftArrow.color = arrowDefaultColor;
        _rightArrow.color = arrowDefaultColor;
        currentCardIndex++;
        if (currentCardIndex > CardDatas.Length - 1) {
            currentCardIndex = CardDatas.Length - 1;
            return;
        }

        if (currentCardIndex == CardDatas.Length - 1) {
            _rightArrow.color = Color.clear;
        }

        StopAllCoroutines();
        startArrowBump(_rightArrow, rightArrowDefaultPosition);
        ShiftCards();
    }

    public void ShiftLeft() {
        _leftArrow.color = arrowDefaultColor;
        _rightArrow.color = arrowDefaultColor;
        currentCardIndex--;
        if (currentCardIndex < 0) {
            currentCardIndex = 0;
            return;
        }

        if (currentCardIndex == 0) {
            _leftArrow.color = Color.clear;
        }

        StopAllCoroutines();
        startArrowBump(_leftArrow, leftArrowDefaultPosition);
        ShiftCards();
    }

    private void ShiftCards() {
        _cycleButtonsAudioSource.Play();
        Vector3 startPosition = CardContainer.transform.localPosition;
        Vector3 endPosition = Vector3.left * (cardTotalSpacing * currentCardIndex);
        StartCoroutine(
            TweenCoroutines.RunAnimationCurveTaperRealTime(.7f, shiftTransformCurve,
                f => { CardContainer.transform.localPosition = Vector3.LerpUnclamped(startPosition, endPosition, f); },
                false,
                () => { CardContainer.transform.localPosition = endPosition; })
        );
    }

    private void startArrowBump(Image arrow, Vector2 defaultPosition) {
        Vector2 up = arrow.rectTransform.up;
        StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(.3f, shiftTransformCurve,
            f => {
                arrow.rectTransform.position = defaultPosition +
                                           up * (shiftTransformCurve.Evaluate(f) * arrowBumpAmplitudeModifier);
            },
            false,
            () => { arrow.rectTransform.position = defaultPosition; }));
    }
}