using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrophobia : MonoBehaviour
{
    [SerializeField]
    private Vector3 secondStepPosition;

    private GameObject player;
    private int step = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        // TODO: Override All functions
    }

    public void WinStep()
    {
        player.transform.position = secondStepPosition;
        step++;
        switch (step)
        {
            case 2:
                break;
        }
    }

}
