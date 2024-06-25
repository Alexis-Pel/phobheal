using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GoalElevator : MonoBehaviour
{
    [SerializeField] private bool isAllowedToMoved = true;
    [SerializeField] private float maxSpeed;

    // Will be remove once the change of difficulties is finished
    public float heightStart;
    public float HeightGoal;

    [SerializeField] private UnityEvent goalEvent;

    [SerializeField] private TMP_Text heightText;

    [Tooltip("Will play when the player try to perform an action he can't perform (yet)")]
    [SerializeField] private AudioSource notAllowedSound;

    [Tooltip("Will play when the player reach the maximum or the minimum Height the cage can do")]
    [SerializeField] private AudioSource hitMaximumRange;

    private float modifier;
    private bool _isDiving;

    public void SetModifier(float modifier)
    {
        //todo: Need Feedback to User
        if (!isAllowedToMoved && !notAllowedSound.isPlaying) notAllowedSound.Play();
        this.modifier = modifier;
    }

    public void AllowedMoving(bool isAllowed)
    {
        isAllowedToMoved = isAllowed;
    }

    private void Start()
    {
        _isDiving = HeightGoal < heightStart;
    }

    void FixedUpdate()
    {
        if (!isAllowedToMoved) return;

        float newHeight = transform.position.y + GetOffset();

        //Depends if the goal is to go up or go down
        float encloseHeight = Mathf.Max(_isDiving ? HeightGoal : heightStart, Mathf.Min(newHeight, _isDiving ? heightStart : HeightGoal));

        if ((encloseHeight == heightStart || encloseHeight == HeightGoal) && transform.position.y != encloseHeight)
        {
            hitMaximumRange.Play();
        }

        transform.position = new Vector3(transform.position.x, encloseHeight, transform.position.z);

        heightText.text = ((transform.position.y - heightStart) / transform.localScale.y).ToString("00");

        if (transform.position.y == HeightGoal && goalEvent != null)
        {
            goalEvent.Invoke();
        }
    }

    private float GetOffset()
    {
        return maxSpeed * (modifier - 0.5f) * 2f;
    }
}
