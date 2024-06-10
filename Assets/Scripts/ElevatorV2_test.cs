using UnityEngine;
using UnityEngine.Events;

public class ElevatorV2_test : MonoBehaviour
{
    // Will be remove once the change of difficulties is finished
    [SerializeField] private float heightStart;
    [SerializeField] private float heightGoal;

    public AudioSource audioSource;
    public float speed;
    private Vector3 newpos;
    [SerializeField] private UnityEvent goalEvent;

    private void Start()
    {
        //_isDiving = heightGoal < heightStart;
        newpos = transform.position;
        newpos.y += heightGoal;
    }

    public void SetModifier(float modifier)
    {
        speed = modifier * 4;
    }

    void Update()
    {
        //if(speed == 0) { return; }

        transform.position = Vector3.MoveTowards(transform.position, newpos, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, heightStart, heightGoal), transform.position.z);

        if (transform.position.y == heightGoal)
        {
            goalEvent.Invoke();
        }
    }
}
