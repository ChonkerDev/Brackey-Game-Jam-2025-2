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
    public float Radius => physicsCollider.radius;
    void Start() {
        trashCanClosedBoolAnimatorHash = Animator.StringToHash("IsClosed");
        _animator.SetBool(trashCanClosedBoolAnimatorHash, false);
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
        
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        EnterTrashcan(PlayerRaccoonComponentContainer);
    }
}
