using System;
using System.Collections;
using Chonker.Core.Tween;
using Chonker.Scripts.Enemy.Enemy_State;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;

public class EnemyDetectionIndicator : MonoBehaviour
{
    [SerializeField] private EnemyAiStateManager _enemyAiStateManager;

    [SerializeField] private SpriteRenderer _detectSpriteRenderer;

    [SerializeField] private SpriteRenderer _lostSpriteRenderer;

    [SerializeField] private AnimationCurve _showIconAnimationCurve;
    [SerializeField] private AudioSource _exclamationAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _detectSpriteRenderer.transform.localScale = Vector3.zero;
        _lostSpriteRenderer.transform.localScale = Vector3.zero;
        _enemyAiStateManager.OnStateChange.AddListener((old, current) => {
            StopAllCoroutines();
            _detectSpriteRenderer.transform.localScale = Vector3.zero;
            _lostSpriteRenderer.transform.localScale = Vector3.zero;
            bool lostDetection = old == EnemyStateId.Chase && current == EnemyStateId.Patrol;
            bool enteredDetection = current == EnemyStateId.Chase && old == EnemyStateId.Patrol;
            if (lostDetection && PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.CurrentState != PlayerStateId.Dead) {
                StartCoroutine(TweenCoroutines.RunAnimationCurveTaper(.3f, _showIconAnimationCurve, f => {
                        _lostSpriteRenderer.transform.localScale = Vector3.one * f;
                    }, false,
                    () => { StartCoroutine(delayHide(_lostSpriteRenderer)); }));
            }

            if (enteredDetection) {
                StartCoroutine(TweenCoroutines.RunAnimationCurveTaper(.3f, _showIconAnimationCurve, f => {
                        _detectSpriteRenderer.transform.localScale = Vector3.one * f;
                    }, false,
                    () => { StartCoroutine(delayHide(_detectSpriteRenderer)); }));
                _exclamationAudioSource.Play();
            }
        });
    }

    private void FixedUpdate() {
        transform.rotation = Quaternion.identity;
    }


    private IEnumerator delayHide(SpriteRenderer spriteRenderer) {
        yield return new WaitForSeconds(2);
        spriteRenderer.transform.localScale = Vector3.zero;
    }
}