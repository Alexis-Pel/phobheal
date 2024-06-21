using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TMP_Text textMeshPro;

    void Start(){
        ElectricMeter._isOn = true;
        SpotLight.SetActive(false);
        Switch._isOn = false;
        textMeshPro.text = "Éteigner toutes les lumières ...\n4/4";

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
        bool _isOn = false;
        foreach (var switchObject in Switchs)
        {
            if(switchObject._isOn) {
                nb_isOn++;
            }
        }
        _isOn = nb_isOn == 0 ? true : false;

        textMeshPro.text = _isOn ? "Aller vous Couchez ?" : $"Éteigner toutes les lumières ...\n{nb_isOn}/4";
        return _isOn;
    }

    bool TriggersBedrooms(){
        if(kenophobiaManager._isInBathroom){
            textMeshPro.text = "Le scénario n°1 est finis ";
            return true;
        }
        return false;
    }

}
