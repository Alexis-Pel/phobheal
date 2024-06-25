using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Step
{
    public GameObject stepIndicator; // Visual indicator for the step
    public GameObject destinationPoint; // Destination point for the step
    public string stepInstruction; // Instruction for the step
    public List<GameObject> objects; // Objects specific to the step
}

public class ScenarioBathroomScript2 : MonoBehaviour
{
    public int IDScénario = 0;
    public int step = 0;
    public float validationDistance = 1.0f; // Distance to validate the objective

    public List<Step> steps; // List of steps
    private int totalSteps;

    [SerializeField]
    private TextMeshProUGUI instructionsText; // TMP Text for displaying instructions

    private Transform playerTransform; // Reference to the player's transform

    public ElectricMeterScript electricMeterScript;

    public StepGameObjectsList Objects;

    void Start()
    {
        electricMeterScript.SetToLightStarted(false);
        totalSteps = steps.Count;
        // Find the Main Camera and use it as the player's transform
        playerTransform = Camera.main.transform;
        ActivateStep(step);
        UpdateInstructions(step);

    }

    void Update()
    {
        step = KenophobiaManager.SwitchStep(step, null, () =>
        {
            if (step >= 0 && step < totalSteps - 1)
            {
                return CheckPlayerAtDestination(steps[step].destinationPoint);
            }
            else if (step == totalSteps - 1)
            {
                return CheckPlayerAtFinalDestination(steps[step].destinationPoint);
            }
            return false;
        });
    }

    bool CheckPlayerAtDestination(GameObject destinationPoint)
    {
        if (IsPlayerAtDestination(destinationPoint))
        {
            //Debug.Log("Player reached destination for step " + step);
            DeactivateStep(step);
            step++;
            if (step < totalSteps)
            {
                ActivateStep(step);
                UpdateInstructions(step);
            }
            return true;
        }
        return false;
    }

    bool CheckPlayerAtFinalDestination(GameObject destinationPoint)
    {
        if (IsPlayerAtDestination(destinationPoint) && AllSwitchesOff())
        {
            //Debug.Log("Player reached final destination and all switches are off.");
            DeactivateStep(step);
            step++;
            UpdateInstructions(step); // Optionally, show a completion message
            return true;
        }
        return false;
    }

    bool IsPlayerAtDestination(GameObject destinationPoint)
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, destinationPoint.transform.position);
            //Debug.Log("Distance to destination: " + distance);
            return distance < validationDistance; // Use the inspector variable
        }
        return false;
    }

    bool AllSwitchesOff()
    {
        SwitchScript[] allSwitchScripts = FindObjectsOfType<SwitchScript>();
        foreach (SwitchScript switchScript in allSwitchScripts)
        {
            if (switchScript._isOn)
            {
                return false;
            }
        }
        return true;
    }

    void ActivateStep(int step)
    {
        if (step >= 0 && step < steps.Count)
        {
            //Debug.Log("Activating step " + step);
            steps[step].stepIndicator.SetActive(true);
            foreach (var obj in steps[step].objects)
            {
                obj.SetActive(true);
            }
        }
    }

    void DeactivateStep(int step)
    {
        if (step >= 0 && step < steps.Count)
        {
            //Debug.Log("Deactivating step " + step);
            steps[step].stepIndicator.SetActive(false);
            foreach (var obj in steps[step].objects)
            {
                obj.SetActive(false);
            }
        }
    }

    void UpdateInstructions(int step)
    {
        if (instructionsText != null)
        {
            if (step >= 0 && step < steps.Count)
            {
                instructionsText.text = steps[step].stepInstruction;
            }
            else
            {
                instructionsText.text = "Scenario completed!"; // Final message or leave blank
            }
        }
    }

}
