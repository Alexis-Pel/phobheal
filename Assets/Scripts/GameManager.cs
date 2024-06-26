using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public EndMenu endMenuCanva;
    public int totalSteps;
    public string[] stepsObjective;
    
    public bool stepEndGame;
    private int stepCompleted = 0;

    [SerializeField] private GameObject pauseScreen;
    public TriggerInputDetector triggerInputDetector;

    public static GameManager Instance; // A static reference to the GameManager instance

    private bool gamePaused = false;
    private bool isWin = false;

    void Awake()
    {
        if (Instance == null) // If there is no instance already
            Instance = this;
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
        //print(stepCompleted);
        stepCompleted++;

        if ((stepCompleted == totalSteps) && stepEndGame)
        {
            WinGame();
        }

        //TODO: Effets de compl�tion des �tapes
    }

    public void WinGame(bool Option = false)
    {
        if (isWin) return;
        isWin = true;
        EndMenu currentEndMenu = Instantiate(endMenuCanva, GameObject.FindGameObjectsWithTag("MainCamera")[0].transform);
        if (stepsObjective.Length > 0)
        {
            currentEndMenu.WriteSentence(stepsObjective[Math.Min(stepCompleted - 1, 0)]);
        }
        
        if(Option)
        {
            currentEndMenu.hideElement();
        }
    }



    /**
     * 
     * PRIVATE METHODS
     * 
     */

    private void ReturnToMenu()
    {
        CancelInvoke();
        isWin = false;
        SceneManager.LoadScene((int)ScenesEnum.MENU, LoadSceneMode.Single);
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
            //Debug.Log("OnEnable");
            triggerInputDetector.ButtonPressed += TogglePause;
        }
    }

    private void OnDisable()
    {
        // Se désabonner de l'événement ButtonPressed
        if (triggerInputDetector != null)
        {
            //Debug.Log("OnDisable");
            triggerInputDetector.ButtonPressed -= TogglePause;
        }
    }

    private void TogglePause()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0 : 1;
        pauseScreen.SetActive(gamePaused); 
    }

}
