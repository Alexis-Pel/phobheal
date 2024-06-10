using UnityEngine;
using UnityEngine.Events;

public class GoalElevator : MonoBehaviour
{
    [SerializeField] private float offsetHeight;

    // Will be remove once the change of difficulties is finished
    [SerializeField] private float heightStart;
    public float HeightGoal;

    [SerializeField] private UnityEvent goalEvent;

    private float modifier;
    private bool _isDiving;

    public void SetModifier(float modifier) => this.modifier = modifier;

    private void Start()
    {
        _isDiving = HeightGoal < heightStart;
    }

    void FixedUpdate()
    {
        float newHeight = transform.position.y + GetOffset();

        //Depends if the goal is to go up or go down
        float encloseHeight = Mathf.Max(_isDiving ? HeightGoal : heightStart, Mathf.Min(newHeight, _isDiving ? heightStart : HeightGoal));
        transform.position = new Vector3(transform.position.x, encloseHeight, transform.position.z);

        if (transform.position.y == HeightGoal)
        {
            goalEvent.Invoke();
        }
    }

    private float GetOffset()
    {
        return offsetHeight * (modifier - 0.5f) * 2f;
    }
}
