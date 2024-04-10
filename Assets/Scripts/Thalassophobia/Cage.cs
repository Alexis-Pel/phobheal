using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] private float offsetHeight;
    private float modifier;
    private float maxHeight;

    [SerializeField] private float depthGoal; // Will be remove once the change of difficulties is finished

    private void Start()
    {
        maxHeight = transform.position.y;
    }
    void Update()
    {
        float newHeight = transform.position.y + GetOffset();
        transform.position = new Vector3(transform.position.x, Mathf.Max(depthGoal, Mathf.Min(newHeight, maxHeight)), transform.position.z);
        if (transform.position.y == depthGoal)
        {
            Debug.Log("Victory !!");
        }
    }

    public void SetModifier(float modifier)
    {
        this.modifier = modifier;
    }

    public float GetOffset()
    {
        return offsetHeight * (modifier - 0.5f) * 2f;
    }
}
