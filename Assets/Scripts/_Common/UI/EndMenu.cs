using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [SerializeField] TMP_Text sentence;
    public void WriteSentence(string lastCheckpointDone)
    {
        sentence.text = "Vous avez réussir à accomplir : \"" + lastCheckpointDone + "\"";
    }
}
