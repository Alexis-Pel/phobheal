using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScenarioTurnElectricMeterScript : MonoBehaviour
{
    public int IDScénario = 3;
    public int step = 0;
    public bool test = false;
    public StepGameObjectsList Objects;
    public ElectricMeterScript electricMeterScript;
    public GameObject trigger;
    public KenophobiaManager kenophobiaManager;
    public TMP_Text textMeshPro;
    public Transform player;

    void Start(){
        // Update Objects and variables at the beginning of the scenario
        player = Camera.main.transform;
        textMeshPro.text = "Aller au compteur \npour réactivé le courant...";
        UpdateStartScenario();

        foreach (var Object in Objects.Steps[step].objects)
        {
            Object.SetActive(true);
        }
    }

    void Update()
    {
        step = KenophobiaManager.SwitchStep(step,Objects, () => { 
            // Add a case like the example 15542 in the switch and in the created case add "return NameOfYourFunction();"
                    switch (step)
                    {
                        case 15542: // Example
                            return true;
                        case 0:
                            return TriggerCompterPosition();
                        case 1:
                            return TriggerElectricMeter();
                        case 2:
                            return TriggerBedroom();
                        default:
                            return ExampleStepOne();
                    }
                });
    }

    bool ExampleStepOne(){
        // Example and test function 
        if (test){
            test = false;
            return true;
        }
        return false;
    }
    bool TriggerCompterPosition(){
        float distance = Vector3.Distance(player.position, new Vector3(8f,1f,5f));
        //Debug.Log(distance);
        if(distance < 2) {
            //Debug.Log("Enter");
            textMeshPro.text = "Réactivez le courant...";
            return true;
        }
        return false;
    }

    bool TriggerElectricMeter(){
        //Debug.Log(electricMeterScript._isOn);
        if (electricMeterScript._isOn){
            textMeshPro.text = "Le courant est réactivé,\n vous pouvez retourner dans votre chambre...";
            electricMeterScript.ActualiseElectricMeter(true);
            return true;
        }
        return false;
    }

    bool TriggerBedroom(){
        float distance = Vector3.Distance(player.position, new Vector3(-2f,0f,5f));
        //Debug.Log(distance);
        if(distance < 2) {
            //Debug.Log("Enter");
            textMeshPro.text = "Bien joué(e) le scénario est finis!";
            return true;
        }
        return false;
    }

    void UpdateStartScenario(){
        electricMeterScript.ActualiseElectricMeter(false);
    }

}
