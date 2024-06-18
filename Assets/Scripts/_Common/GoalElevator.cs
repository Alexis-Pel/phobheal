using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GoalElevator : MonoBehaviour
{
    [SerializeField] private bool isAllowedToMoved = true;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float heightStart;
    public float HeightGoal;

    [SerializeField] private UnityEvent goalEvent;

    [SerializeField] private TMP_Text heightText;

    [Tooltip("Will play when the player try to perform an action he can't perform (yet)")]
    [SerializeField] private AudioSource notAllowedSound;

    [Tooltip("Will play when the player reach the maximum or the minimum Height the cage can do")]
    [SerializeField] private AudioSource hitMaximumRange;

    private float modifier;
    private bool _isDiving;

    private bool hasPlayedNotAllowedSound;

    public void SetModifier(float modifier) => this.modifier = modifier;

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
        //todo: Need Feedback to User
        if (!isAllowedToMoved)
        {
            if (!hasPlayedNotAllowedSound)
            {
                hasPlayedNotAllowedSound = true;
                //todo: play not allowed sound
            }
            return;
        }
        hasPlayedNotAllowedSound = false;

        float newHeight = transform.position.y + GetOffset();

        //Depends if the goal is to go up or go down
        float encloseHeight = Mathf.Max(_isDiving ? HeightGoal : heightStart, Mathf.Min(newHeight, _isDiving ? heightStart : HeightGoal));

        if ((encloseHeight == heightStart || encloseHeight == HeightGoal) && transform.position.y != encloseHeight)
        {
            //todo: play sound that define we are at the maximum or minimum we can go
        }

        transform.position = new Vector3(transform.position.x, encloseHeight, transform.position.z);

        heightText.text = ((transform.position.y - heightStart) / transform.localScale.y).ToString("00");

        if (transform.position.y == HeightGoal)
        {
            goalEvent.Invoke();
        }
    }

    private float GetOffset()
    {
        return maxSpeed * (modifier - 0.5f) * 2f;
    }
}
