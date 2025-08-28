using System;
using UnityEngine;

public class TransformBob : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float _loopTime = 1;
    [SerializeField] private float amplitudeMultiplier = 1;
    [SerializeField] private Vector3 Axis = Vector3.up;
    [SerializeField] private bool _localSpace;

    private float currentAlpha;
    private Vector3 originalPosition;

    private void Awake() {
        originalPosition = transform.position;
    }

    public void SetBasePosition(Vector3 position) {
        originalPosition = position;
    }


    private void Update() {
        currentAlpha += Time.deltaTime / _loopTime;
        currentAlpha %= 1;
        float eval = curve.Evaluate(currentAlpha) * amplitudeMultiplier;
        if (_localSpace) {
            Vector3 localAxis = transform.InverseTransformDirection(Axis);
            transform.localPosition = originalPosition + localAxis * eval;
        }
        else {
            transform.position = originalPosition + Axis * eval;
        }
    }
}