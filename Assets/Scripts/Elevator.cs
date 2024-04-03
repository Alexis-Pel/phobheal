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


    int i;
    bool reverse;

    private void Start()
    {
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
        {
            canMove = false;

            if (i == points.Length - 1)
            {
                reverse = true;
                i--;
                return;
            }
            else if (i == 0)
            {
                reverse = true;
                i++;
                return;
            }
            if(reverse)
            {
                i--;
            }
            else
            {
                i++;
            }
        }
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canMove = true;
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        canMove = false;
        other.transform.SetParent(null);
    }
}
