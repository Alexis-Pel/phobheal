using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrophobia : MonoBehaviour
{
    [SerializeField]
    private Vector3 secondStepPosition;

    private GameObject player;
    private GameObject gameManager;
    private int step = 1;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectsWithTag("Player")[0];
        gameManager = GameObject.FindGameObjectsWithTag("GameController")[0];
    }

    public void WinStep()
    {
        step++;
        switch (step)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                gameManager.GetComponent<GameManager>().WinGame();
                break;
            default:
                break;
        }
    }

}
