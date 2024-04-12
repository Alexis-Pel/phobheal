using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ElevatorV2_test : MonoBehaviour
{
    // Will be remove once the change of difficulties is finished
    [SerializeField] private float heightStart;
    [SerializeField] private float heightGoal;

    public XRSlider slider;

    public float speed;
    private Vector3 newpos;

    private void Start()
    {
        //_isDiving = heightGoal < heightStart;
        newpos = transform.position;
        newpos.y += heightGoal;
    }

    public void changeSpeed()
    {
        this.speed = slider.value * 10;
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, newpos, speed * Time.deltaTime);
        print(heightGoal + " " + transform.position.y);

        if (transform.position.y >= heightGoal)
        {
            GameManager.Instance.WinGame();
        }
    }
}
