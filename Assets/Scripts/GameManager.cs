using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canva;
    public int totalSteps;
    public GameObject pauseScreen;
    public TriggerInputDetector triggerInputDetector;
    public string[] stepsObjective;

    private ScenesEnum currentScenarioIndex;
    private bool gamePaused = false;
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
        stepCompleted++;

        if (stepCompleted == totalSteps)
        {
            WinGame();
        }

        //TODO: Effets de compl�tion des �tapes
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
        Destroy(gameObject);
        CancelInvoke();
        SceneManager.LoadScene(((int)ScenesEnum.MENU), LoadSceneMode.Single);
    }

    public void StopGame()
    {
        ReturnToMenu();
    }

    // public delegate void Delegate();
    // public Delegate WinStep;

    private void OnEnable()
    {
        // S'abonner à l'événement ButtonPressed
        if (triggerInputDetector != null)
        {
            Debug.Log("OnEnable");
            triggerInputDetector.ButtonPressed += TogglePause;
        }
    }

    private void OnDisable()
    {
        // Se désabonner de l'événement ButtonPressed
        if (triggerInputDetector != null)
        {
            Debug.Log("OnDisable");
            triggerInputDetector.ButtonPressed -= TogglePause;
        }
    }

        private void TogglePause()
    {
        Debug.Log("TogglePause !!!");
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0 : 1;
        pauseScreen.SetActive(gamePaused); 
    }

}
