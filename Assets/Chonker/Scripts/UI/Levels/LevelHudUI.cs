using System;
using System.Collections.Generic;
using Chonker.Core.Attributes;
using Chonker.Core.Tween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelHudUI : MonoBehaviour
{
    private LevelManager levelManager;

    [Header("Biscuits")] [SerializeField, PrefabModeOnly] private Image BiscuitCountTemplate;
    [SerializeField, PrefabModeOnly] private HorizontalLayoutGroup biscuitsGroup;
    [SerializeField, PrefabModeOnly] private TextMeshProUGUI escapeAvailableText;
    [SerializeField] private AnimationCurve biscuitCollectedScaleCurve;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _readyForEscapeSoundClip;

    private Color ShowBiscuitColor = Color.white;
    private Color HideBiscuitColor = Color.gray3;
    private List<Image> biscuitsCollected;

    private float biscuitCollectedEffectTime = .3f;

    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }


    void Start() {
        escapeAvailableText.gameObject.SetActive(false);
        biscuitsCollected = new List<Image>();
        for (int i = 0; i < levelManager.numBiscuits; i++) {
            biscuitsCollected.Add(Instantiate(BiscuitCountTemplate, biscuitsGroup.transform));
            biscuitsCollected[i].color = HideBiscuitColor;
        }
        
        Destroy(BiscuitCountTemplate.gameObject);

        levelManager.OnBiscuitCollected.AddListener((numBiscuits) => {
            for (var i = 0; i < biscuitsCollected.Count; i++) {
                Image biscuit = biscuitsCollected[i];
                if (i == numBiscuits - 1) {
                    StartCoroutine(TweenCoroutines.RunAnimationCurveTaperRealTime(biscuitCollectedEffectTime, biscuitCollectedScaleCurve, f => {
                        biscuit.transform.localScale = Vector3.one * f;
                    }, false, () => {
                        biscuit.transform.localScale = Vector3.one;
                    }));
                    StartCoroutine(TweenCoroutines.RunTaperRealTime(biscuitCollectedEffectTime, f => {
                        float angleRange = 10;
                        float wiggles = 3f;
                        float angle = Mathf.Sin(f * Mathf.PI * 2f * wiggles) * (1f - f) * angleRange;
                        biscuit.rectTransform.Rotate(Vector3.forward * angle);
                    },
                    () => {
                        biscuit.rectTransform.localEulerAngles = Vector3.zero;
                    }));
                }
                if (i < numBiscuits) {
                    biscuit.color = ShowBiscuitColor;
                }
                else {
                    biscuit.color = HideBiscuitColor;
                }

                if (levelManager.CanExitLevel) {
                    _audioSource.PlayOneShot(_readyForEscapeSoundClip);
                    escapeAvailableText.gameObject.SetActive(true);
                }
            }
        });
    }
}