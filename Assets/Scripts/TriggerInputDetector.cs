using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using TMPro;

public class TriggerInputDetector : MonoBehaviour
{
    public TextMeshProUGUI leftScoreDisplay;
    public TextMeshProUGUI rightScoreDisplay;

    private InputData _inputData;
    private float _leftMaxScore = 0f;
    private float _rightMaxScore = 0f;
    private float triggerValue;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
        Debug.Log("Started inputData: " + _inputData);
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

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out rightTriggerValue) || _inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue))
        {
            // Utiliser la valeur de trigger la plus élevée des deux contrôleurs
            triggerValue = Mathf.Max(rightTriggerValue, leftTriggerValue);
            leftScoreDisplay.text = triggerValue.ToString("#.00");
            Debug.Log("triggerValue: " + triggerValue);
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out rightAbutton) || _inputData._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out leftAbutton))
        {
            // Utiliser la valeur booléenne de l'un des boutons A (s'ils sont pressés)
            bool Abutton = rightAbutton || leftAbutton;
            rightScoreDisplay.text = Abutton.ToString();
            Debug.Log("A button: " + Abutton);
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out rightBbutton) || _inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out leftBbutton))
        {
            // Utiliser la valeur booléenne de l'un des boutons B (s'ils sont pressés)
            bool Bbutton = rightBbutton || leftBbutton;
            rightScoreDisplay.text = Bbutton.ToString();
            Debug.Log("B button: " + Bbutton);
        }

        //triggerValue = ((float)_inputData._leftController.characteristics);
        //Debug.Log("triggerValue is: " + triggerValue);

        //if (_inputData._leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        //{
        //    _leftMaxScore = Mathf.Max(leftVelocity.magnitude, _leftMaxScore);
        //    leftScoreDisplay.text = _leftMaxScore.ToString("F2");
        //}
        //if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        //{
        //    _rightMaxScore = Mathf.Max(rightVelocity.magnitude, _rightMaxScore);
        //    rightScoreDisplay.text = _rightMaxScore.ToString("F2");
        //}
    }
}
