using System;
using System.Collections.Generic;
using Chonker.Core;
using Chonker.Core.Tween;
using Chonker.Scripts.Management;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private HorizontalLayoutGroup CardContainer;
    [SerializeField] private LevelSelectCard CardTemplate;
    [SerializeField] private AnimationCurve shiftTransformCurve;
    [SerializeField] private Button _playButton;

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
        Destroy(CardTemplate.gameObject);
        gameObject.SetActive(false);
    }

    public void ShiftRight() {
        StopAllCoroutines();
        currentCardIndex++;
        currentCardIndex = Mathf.Clamp(currentCardIndex, 0, CardDatas.Length - 1);
        if (currentCardIndex > CardDatas.Length - 1) {
            currentCardIndex = CardDatas.Length - 1;
            return;
        }

        ShiftCards();
    }

    public void ShiftLeft() {
        currentCardIndex--;
        if (currentCardIndex < 0) {
            currentCardIndex = 0;
            return;
        }

        StopAllCoroutines();
        ShiftCards();
    }

    private void ShiftCards() {
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