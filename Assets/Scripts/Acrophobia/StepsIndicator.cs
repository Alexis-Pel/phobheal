using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Linq;

public class StepsIndicator : MonoBehaviour
{
    [SerializeField]
    private TMP_Text heightText;

    public List<float> stepsAltitude;

    private void Start()
    {
        GameManager.Instance.totalSteps = stepsAltitude.Count;
    }

    private void Update()
    {
        for (int i = 0; i < stepsAltitude.Count; i++) 
        { 
            if(stepsAltitude[i] == float.Parse(heightText.text))
            {
                stepsAltitude.RemoveAt(i);
                GameManager.Instance.StepDone();
            }
        }
    }
}
