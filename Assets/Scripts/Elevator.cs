using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Elevator : MonoBehaviour
{
    public bool canMove;
    [SerializeField]
    private float speed;

    [SerializeField]
    private int startingPoint;

    [SerializeField]
    private Transform[] points;

    private void Start()
    {
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[1].position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, points[0].position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        canMove = true;
        other.transform.parent.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        canMove = false;
        other.transform.parent.SetParent(null);
    }
}
