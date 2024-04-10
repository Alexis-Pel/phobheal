using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ScenesEnum currentScenarioIndex;
    public GameObject player;
    public GameObject player_locomotion;
    public GameObject canva;

    // Start is called before the first frame update
    void Start()
    {
        LoadNewScenario(Settings.SceneToLoad);
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

    /// <summary>
    /// Change current scenario
    /// </summary>
    /// <param name="newScenario">the name of the scene to be loaded</param>
    public void ChangeScenario(ScenesEnum scene)
    {
        // Reinitialisation position joueur ? -> Fondu au noir
        UnloadCurrentScenario();
        LoadNewScenario((int) scene);
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

    private void LoadNewScenario(int scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneAt(scene));
        player.transform.position = Settings.startPosition;
    }

    // public delegate void Delegate();
    // public Delegate WinStep;

}
