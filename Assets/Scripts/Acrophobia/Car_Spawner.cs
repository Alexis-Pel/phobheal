using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cars;

    [SerializeField]
    private float speed = 50f;

    [SerializeField]
    private float objective;

    private bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        if(cars.Length > 0)
        {
            on = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
           on = false;
           int range = Random.Range(0, 11);
           Invoke(nameof(SpawnCars), range);
        }
    }

    public void SpawnCars() {
        int range = Random.Range(0, cars.Length);
        GameObject inst = Instantiate(cars[range], position: transform.position, rotation: transform.rotation);
        Car script = inst.AddComponent<Car>();

        script.speed = speed;
        script.objective = objective;

        on = true;
    }

    public void StopSpawnCars()
    {
        on = false;
        CancelInvoke();
    }
}