using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ScenesEnum currentScenarioIndex;
    public GameObject canva;

    public static GameManager Instance; // A static reference to the GameManager instance

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
    *   
    * PUBLIC METHODS
    * 
    */

    public void WinGame()
    {
        // TODO: Show win screen
        Instantiate(canva, GameObject.FindGameObjectsWithTag("MainCamera")[0].transform);
        Invoke(nameof(StopGame), 5f);
    }

    private void StopGame()
    {
        // TODO: Stop the game -> Return to menu ?
        ReturnToMenu();
    }


    public void WinStep()
    {
        // TODO: Win Step
    }


    /**
     * 
     * PRIVATE METHODS
     * 
     */

    private void ReturnToMenu()
    {
        CancelInvoke();
        SceneManager.LoadScene(((int)ScenesEnum.MENU), LoadSceneMode.Single);
    }

    // public delegate void Delegate();
    // public Delegate WinStep;

}
