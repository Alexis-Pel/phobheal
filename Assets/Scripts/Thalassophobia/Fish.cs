using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private FocusObject focus;
    [SerializeField] private float lifeTime;
    private float speed;

    private void Start()
    {
        speed = focus.Speed/2f; // Trying to not reach the focus object
        Invoke(nameof(SelfDestruct), lifeTime);
    }
    void FixedUpdate()
    {
        if (focus == null) return;
        transform.LookAt(focus.transform);
        transform.position += speed * Time.deltaTime * transform.forward;
    }
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
