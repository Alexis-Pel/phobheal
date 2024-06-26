using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndMenu : MonoBehaviour
{
    [SerializeField] TMP_Text sentence;

        [SerializeField] GameObject HideElement;
    public void WriteSentence(string lastCheckpointDone)
    {
        sentence.text = "Vous avez réussir à accomplir : \"" + lastCheckpointDone + "\"";
    }

        public void reloadedScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void hideElement()
    {
        if (HideElement == null) return;
        
        HideElement.SetActive(false);
    }
}
