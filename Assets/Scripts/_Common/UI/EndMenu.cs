using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndMenu : MonoBehaviour
{
    [SerializeField] TMP_Text sentence;
    public void WriteSentence(string lastCheckpointDone)
    {
        sentence.text = "Vous avez r�ussir � accomplir : \"" + lastCheckpointDone + "\"";
    }

        public void reloadedScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
