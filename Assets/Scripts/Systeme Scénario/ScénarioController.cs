using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    public GameObject[] scenarioSteps; // Assignez les étapes dans l'inspecteur
    public int currentStepIndex = 0; // Etape du scénario


    public void NextStep()
    {
        if (currentStepIndex <= 0)
        {
            scenarioSteps[currentStepIndex].SetActive(false);
            currentStepIndex++;
        }
        else if (currentStepIndex < scenarioSteps.Length - 1)
        {
            scenarioSteps[currentStepIndex].SetActive(false);
            currentStepIndex++;
        }
        else
        {
            EndTutorial();
        }
    }
        private void EndTutorial()
    {
        // Logique de fin du tutoriel
        Debug.Log("Tutoriel terminé !");
    }
}
