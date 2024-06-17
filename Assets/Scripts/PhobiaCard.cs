using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhobiaCard : MonoBehaviour
{
    public PhobiaScriptable phobiaScriptable;

    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text phobiaNameUI;

    // Start is called before the first frame update
    void Start()
    {
        phobiaNameUI.text = phobiaScriptable.m_name;
        if(phobiaScriptable.m_image != null)
        {
            image.sprite = phobiaScriptable.m_image;
        }
        else
        {
            print(phobiaScriptable.m_name);
            print(phobiaScriptable.m_image);
            image.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    public void GoToScenario()
    {
        Settings.startPosition = phobiaScriptable.m_StartPosition;
        Settings.instructionNextScene = (int)phobiaScriptable.m_scene;

        SceneManager.LoadScene(sceneName: "instructions");
    }
}
