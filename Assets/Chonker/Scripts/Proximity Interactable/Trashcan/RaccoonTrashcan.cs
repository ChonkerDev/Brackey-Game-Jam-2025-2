using System;
using System.Collections;
using Chonker.Scripts.Player_Raccoon;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class RaccoonTrashcan : ProximityInteractable
{
    [SerializeField] private AudioClip _openSound;
    [SerializeField] private AudioClip _closeSound;
    [SerializeField] private Animator _animator;
    private int trashCanClosedBoolAnimatorHash;
    [SerializeField] private CircleCollider2D physicsCollider;
    [SerializeField] private TransformBob _indicatorBob;
    public float Radius => physicsCollider.radius;
    private float indicatorOffset = .25f;
    void Start() {
        trashCanClosedBoolAnimatorHash = Animator.StringToHash("IsClosed");
        _animator.SetBool(trashCanClosedBoolAnimatorHash, false);
        _indicatorBob.transform.up = Vector2.up;
        Vector2 indicatorPos = transform.position;
        _indicatorBob.SetBasePosition(indicatorPos + Vector2.up * indicatorOffset);
        _indicatorBob.gameObject.SetActive(false);
    }

    public void EnterTrashcan(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        _audioSource.PlayOneShot(_closeSound);
        _animator.SetBool(trashCanClosedBoolAnimatorHash, true);
    }

    public void ExitTrashcan() {
        _audioSource.PlayOneShot(_openSound);
        _animator.SetBool(trashCanClosedBoolAnimatorHash, false);
    }

    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        _indicatorBob.gameObject.SetActive(true);
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        _indicatorBob.gameObject.SetActive(false);
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        EnterTrashcan(PlayerRaccoonComponentContainer);
    }
}
