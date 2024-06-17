using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class instruction : MonoBehaviour
{
    public TMP_Text instructionMessagePlaceholder;

    // Start is called before the first frame update
    void Start()
    {
        string message;
        switch (Settings.instructionNextScene)
        {
            case (int)ScenesEnum.MENU:
                message = Settings.baseInstruction;
                break;

            default:
                message = Settings.experienceInstruction;
                break;
        }
        instructionMessagePlaceholder.text = message;
    }

    public void nextScene()
    {
        SceneManager.LoadScene(Settings.instructionNextScene);
    }
}
