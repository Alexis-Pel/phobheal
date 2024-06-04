using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioExampleScript : MonoBehaviour
{
    public int step = 0;
    public bool test = false;
    public StepGameObjectsList Objects;

    void Start(){
        // Update Objects and variables at the beginning of the scenario
    }

    void Update()
    {
        step = KenophobiaManager.SwitchStep(step,Objects, () => { 
            // Add a case like the example 15542 in the switch and in the created case add "return NameOfYourFunction();"
                    switch (step)
                    {
                        case 15542: // Example
                            return true;
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

}
