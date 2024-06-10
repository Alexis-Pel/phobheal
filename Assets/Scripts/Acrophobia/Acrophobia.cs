using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        // gameManager = GameObject.FindGameObjectsWithTag("GameController")[0];
    }

    public void WinGame()
    {
        gameManager.GetComponent<GameManager>().WinGame();
    }

}
