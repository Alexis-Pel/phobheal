using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string currentScenarioName;

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
    }

    public void StopGame()
    {
        // TODO: Stop the game -> Return to menu ?
        ReturnToMenu();
    }

    /// <summary>
    /// Change current scenario
    /// </summary>
    /// <param name="newScenario">the name of the scene to be loaded</param>
    public void ChangeScenario(string newScenario)
    {
        // Reinitialisation position joueur ? -> Fondu au noir
        UnloadCurrentScenario();
        LoadNewScenario(newScenario);
        currentScenarioName = newScenario;
    }


    /**
     * 
     * PRIVATE METHODS
     * 
     */

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(Settings.MENU_SCENE_NAME, LoadSceneMode.Single);
    }

    private void UnloadCurrentScenario()
    {
        SceneManager.UnloadSceneAsync(currentScenarioName);
    }

    private void LoadNewScenario(string newScenario)
    {
        SceneManager.LoadScene(newScenario, LoadSceneMode.Additive);
    }
}
