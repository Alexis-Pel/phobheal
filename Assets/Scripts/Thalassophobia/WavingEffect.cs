using UnityEngine;

public class WavingEffect : MonoBehaviour
{
    [SerializeField] private float heightProcessTime;
    [SerializeField] private float heightLimit;
    [SerializeField] private Vector3 angleProcessTime = Vector3.zero;
    [SerializeField] private Vector3 angleLimit = Vector3.zero;
    [SerializeField] private AnimationCurve animationCurve;

    private void Start()
    {
        animationCurve.postWrapMode = WrapMode.PingPong;
        animationCurve.preWrapMode = WrapMode.PingPong;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angleOffset = new (
            angleLimit.x != 0 ? angleLimit.x * animationCurve.Evaluate(Time.time / angleProcessTime.x) : transform.localRotation.eulerAngles.x,
            angleLimit.y != 0 ? angleLimit.y * animationCurve.Evaluate(Time.time / angleProcessTime.y) : transform.localRotation.eulerAngles.y,
            angleLimit.z != 0 ? angleLimit.z * animationCurve.Evaluate(Time.time / angleProcessTime.z) : transform.localRotation.eulerAngles.z
        );

        float height = heightLimit != 0 ? heightLimit * animationCurve.Evaluate(Time.time / heightProcessTime) : transform.position.y;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        transform.localRotation = Quaternion.Euler(angleOffset);
    }
}
