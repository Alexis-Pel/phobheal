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
            image.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    public void GoToScenario()
    {
        Settings.SceneToLoad = (int)phobiaScriptable.m_scene;
        Settings.startPosition = phobiaScriptable.m_StartPosition;
        SceneManager.LoadScene(phobiaScriptable.m_name, LoadSceneMode.Single);
    }
}
