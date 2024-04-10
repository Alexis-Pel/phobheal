using UnityEngine;

public class WavingEffect : MonoBehaviour
{
    [SerializeField] private float heightProcessTime;
    [SerializeField] private float heightLimit;
    [SerializeField] private Vector3 angleProcessTime = Vector3.zero;
    [SerializeField] private Vector3 angleLimit = Vector3.zero;
    [SerializeField] private AnimationCurve animationCurve;

    private float FromPercentage(float max, float percentage)
    {
        return max * percentage;
    }

    private void Start()
    {
        animationCurve.postWrapMode = WrapMode.PingPong;
        animationCurve.preWrapMode = WrapMode.PingPong;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angleOffset = new Vector3(
            angleLimit.x != 0 ? FromPercentage(angleLimit.x, animationCurve.Evaluate(Time.time / angleProcessTime.x)) : transform.rotation.eulerAngles.x,
            angleLimit.y != 0 ? FromPercentage(angleLimit.y, animationCurve.Evaluate(Time.time / angleProcessTime.y)) : transform.rotation.eulerAngles.y,
            angleLimit.z != 0 ? FromPercentage(angleLimit.z, animationCurve.Evaluate(Time.time / angleProcessTime.z)) : transform.rotation.eulerAngles.z
        );
        transform.SetPositionAndRotation(
            new Vector3(transform.position.x, FromPercentage(heightLimit, animationCurve.Evaluate(Time.time / heightProcessTime)), transform.position.z),
            Quaternion.Euler(angleOffset)
        );
    }
}
