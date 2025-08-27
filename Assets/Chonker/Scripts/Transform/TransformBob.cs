using System;
using UnityEngine;

public class TransformBob : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float _loopTime = 1;
    [SerializeField] private float amplitudeMultiplier = 1;
    [SerializeField] private Vector3 Axis = Vector3.up;
    
    private float currentAlpha;


    private void Update() {
        currentAlpha += Time.deltaTime / _loopTime;
        currentAlpha %= 1;
        float eval = curve.Evaluate(currentAlpha) * amplitudeMultiplier;
        transform.localPosition = Axis * eval;
    }
}