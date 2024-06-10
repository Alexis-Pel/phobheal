using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;
    public float objective;

    // Update is called once per frame
    private void FixedUpdate()
    {   if (transform.position.z == objective)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, objective), Time.deltaTime * speed);
        }
    }
}
