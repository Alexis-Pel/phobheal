using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] fishes;
    [SerializeField] private GameObject focus;
    [SerializeField] private float lifeTime;
    [SerializeField] private float fishSpeed;

    private void Start()
    {
        Invoke(nameof(SelfDestruct), lifeTime);
    }

    private void FixedUpdate()
    {
        //focus.transform.position += fishSpeed * Time.deltaTime * transform.forward;

        for (int i = 0; i < fishes.Length; i++)
        {
            GameObject fish = fishes[i];
            fish.transform.LookAt(focus.transform);
            //fish.transform.position += fishSpeed * Time.deltaTime * transform.forward;
        }

    }

    private void SelfDestruct()
    {
        //Destroy(gameObject);
    }
}
