using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] private float offsetHeight;
    private float modifier;
    private float maxHeight;
    // Update is called once per frame

    private void Start()
    {
        maxHeight = transform.position.y;
    }
    void Update()
    {
        float newHeight = transform.position.y + GetOffset();
        transform.position = new Vector3(transform.position.x, Mathf.Min(newHeight, maxHeight), transform.position.z);
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
