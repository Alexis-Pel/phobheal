using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using TMPro;
using System;

public class TriggerInputDetector : MonoBehaviour
{
    //public TextMeshProUGUI leftScoreDisplay;
    //public TextMeshProUGUI rightScoreDisplay;

    private InputData _inputData;
    private float _leftMaxScore = 0f;
    private float _rightMaxScore = 0f;
    private float triggerValue;


    public event Action ButtonPressed;
    private bool buttonPreviouslyPressed = false;


    private void Start()
    {
        _inputData = GetComponent<InputData>();
        //Debug.Log("Started inputData: " + _inputData);
    }

    // Update is called once per frame
    void Update()
    {
        // Initialiser les variables
        float leftTriggerValue = 0f;
        float rightTriggerValue = 0f;
        bool leftAbutton = false;
        bool rightAbutton = false;
        bool leftBbutton = false;
        bool rightBbutton = false;

        bool isButtonPressed = false;

        //if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out rightTriggerValue) || _inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue))
        //{
        //    // Use the highest trigger value of the two controllers
        //    triggerValue = Mathf.Max(rightTriggerValue, leftTriggerValue);
        //    //leftScoreDisplay.text = triggerValue.ToString("#.00");
        //    Debug.Log("triggerValue: " + triggerValue);

        //    if (triggerValue > 0.5)
        //    {
        //        isButtonPressed = true;
        //    }
        //}

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out rightAbutton) || _inputData._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out leftAbutton))
        {
            // Use the boolean value of one of the A buttons (if they are pressed)
            bool Abutton = rightAbutton || leftAbutton;
            //rightScoreDisplay.text = Abutton.ToString();
            //Debug.Log("A button: " + Abutton);

            if (Abutton)
            {
                isButtonPressed = true;
            }
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out rightBbutton) || _inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out leftBbutton))
        {
            // Use the boolean value of one of the B buttons (if they are pressed)
            bool Bbutton = rightBbutton || leftBbutton;
            //rightScoreDisplay.text = Bbutton.ToString();
            //Debug.Log("B button: " + Bbutton);
            
            if (Bbutton)
            {
                isButtonPressed = true;
            }
        }

        // Déclencher l'événement uniquement si un bouton est appuyé et n'était pas appuyé précédemment
        if (isButtonPressed && !buttonPreviouslyPressed)
        {
            //Debug.Log("TriggerInputDetector: Invoking ButtonPressed event");
            ButtonPressed?.Invoke();
        }

        // Mettre à jour l'état précédent des boutons
        buttonPreviouslyPressed = isButtonPressed;
    }
}
