using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartNiveau1()
    {
        PlayerPrefs.SetInt("SelectedLevel", 1);
        SceneManager.LoadScene(((int)ScenesEnum.KENOPHOBIA), LoadSceneMode.Single);
    }

    public void StartNiveau2()
    {
        PlayerPrefs.SetInt("SelectedLevel", 2);
        SceneManager.LoadScene(((int)ScenesEnum.KENOPHOBIA), LoadSceneMode.Single);
    }

    public void StartNiveau3()
    {
        PlayerPrefs.SetInt("SelectedLevel", 3);
        SceneManager.LoadScene(((int)ScenesEnum.KENOPHOBIA), LoadSceneMode.Single);
    }
}
