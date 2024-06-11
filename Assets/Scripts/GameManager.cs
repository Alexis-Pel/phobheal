using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canva;
    public int totalSteps;

    private ScenesEnum currentScenarioIndex;
    private int stepCompleted = 0;

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


    public int StepCompleted
    {
        get { return stepCompleted; }
        // set { stepCompleted = value; }
    }


    /**
    *   
    * PUBLIC METHODS
    * 
    */

    public void StepDone()
    {
        print(stepCompleted);
        stepCompleted++;

        if (stepCompleted == totalSteps)
        {
            WinGame();
        }

        //TODO: Effets de complétion des étapes
    }

    public void WinGame()
    {
        // TODO: Show win screen
        Instantiate(canva, GameObject.FindGameObjectsWithTag("MainCamera")[0].transform);
        Invoke(nameof(StopGame), 5f);
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

    private void StopGame()
    {
        ReturnToMenu();
    }

    // public delegate void Delegate();
    // public Delegate WinStep;

}
