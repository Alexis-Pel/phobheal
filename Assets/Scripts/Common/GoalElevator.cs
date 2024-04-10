using UnityEngine;

public class GoalElevator : MonoBehaviour
{
    [SerializeField] private float offsetHeight;

    // Will be remove once the change of difficulties is finished
    [SerializeField] private float heightStart;
    [SerializeField] private float heightGoal;

    private float modifier;
    private bool _isDiving;

    public void SetModifier(float modifier)
    {
        this.modifier = modifier;
    }

    private void Start()
    {
        _isDiving = heightGoal < heightStart;
    }

    void Update()
    {
        float newHeight = transform.position.y + GetOffset();
        //Depends if the goal is to go up or go down
        float encloseHeight = Mathf.Max(_isDiving ? heightStart : heightGoal, Mathf.Min(newHeight, _isDiving ? heightGoal : heightStart));
        transform.position = new Vector3(transform.position.x, encloseHeight, transform.position.z);

        if (transform.position.y == heightGoal)
        {
            GameManager.Instance.WinGame();
        }
    }

    private float GetOffset()
    {
        return offsetHeight * (modifier - 0.5f) * 2f;
    }
}
