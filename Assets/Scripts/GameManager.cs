using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ScenesEnum currentScenarioIndex;

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
    public void ChangeScenario(ScenesEnum scene)
    {
        // Reinitialisation position joueur ? -> Fondu au noir
        UnloadCurrentScenario();
        LoadNewScenario(scene);
        currentScenarioIndex = scene;
    }


    /**
     * 
     * PRIVATE METHODS
     * 
     */

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(((int)ScenesEnum.MENU), LoadSceneMode.Single);
    }

    private void UnloadCurrentScenario()
    {
        SceneManager.UnloadSceneAsync(((int)currentScenarioIndex));
    }

    private void LoadNewScenario(ScenesEnum scene)
    {
        SceneManager.LoadScene(((int)scene), LoadSceneMode.Additive);
    }
}
