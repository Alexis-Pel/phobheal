using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioLightTurnScript : MonoBehaviour
{
    public int IDScénario = 0;
    public int step = 0;
    public bool test = false;
    public StepGameObjectsList Objects;
    public ElectricMeterScript ElectricMeter;
    public GameObject SpotLight;
    public SwitchScript Switch;
    public int nbLigth = 0;
    public List<SwitchScript> Switchs;
    public KenophobiaManager kenophobiaManager;

    void Start(){
        ElectricMeter._isOn = true;
        SpotLight.SetActive(false);
        Switch._isOn = false;

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
                            return TriggersLights();
                        case 1:
                            return TriggersBedrooms();
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

    bool TriggersLights(){
        int nb_isOn = 0;
        foreach (var switchObject in Switchs)
        {
            if(switchObject._isOn) {
                nb_isOn++;
            }
        }
        return nb_isOn == 0 ? true : false;
    }

    bool TriggersBedrooms(){
        if(kenophobiaManager._isInBathroom){
            return true;
        }
        return false;
    }

}
