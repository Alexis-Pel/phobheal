using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ElevatorV2_test : MonoBehaviour
{
    // Will be remove once the change of difficulties is finished
    [SerializeField] private float heightStart;
    [SerializeField] private float heightGoal;

    public XRSlider slider;
    public AudioSource audioSource;
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
        speed = slider.value * 10;
        if (speed == 0)
        {
            StopClip();
            PlayCLip();
            Invoke(nameof(StopClip), 1f);
        }
    }

    void Update()
    {
        //if(speed == 0) { return; }

        transform.position = Vector3.MoveTowards(transform.position, newpos, speed * Time.deltaTime);

        if (transform.position.y >= heightGoal)
        {
            speed = 0;
            GameManager.Instance.WinGame();
        }
    }

    private void StopClip()
    {
        audioSource.Stop();
    }

    private void PlayCLip()
    {
        audioSource.Play();
    }
}
