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
        string scene = null;
        switch (phobiaScriptable.m_scene)
        {
            case PhobiasEnum.ACROPHOBIA:
                scene = Settings.ACROPHOBIA_SCENE_NAME;
                break;
            case PhobiasEnum.CLAUSTROPHOBIA:
                scene = Settings.CLAUSTROPHOBIA_SCENE_NAME;
                break;
            default:
                break;
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
