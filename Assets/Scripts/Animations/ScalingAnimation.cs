using UnityEngine;
using UnityEngine.Events;

public class ScalingAnimation : MonoBehaviour
{
    public AnimationCurve curve;
    public float speed = 1;
    public Vector3 startScale = Vector3.zero;
    public Vector3 targetScale = Vector3.one;

    private float _timeAggregate;

    public UnityEvent animationFinishEvent;
    
    private void OnEnable()
    {
        _timeAggregate = 0;
        transform.localScale = startScale;
    }

    private void Update()
    {
        _timeAggregate += Time.deltaTime * speed;
        float value = curve.Evaluate(_timeAggregate);
        transform.localScale = Vector3.LerpUnclamped(startScale, targetScale, value);
        if (_timeAggregate >= 1)
        {
            transform.localScale = targetScale;
            enabled = false;
            animationFinishEvent.Invoke();
        }
    }
}
