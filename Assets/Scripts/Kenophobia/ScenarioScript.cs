using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScript : MonoBehaviour
{
    public int scenario;
    public int step;
    public StepGameObjectsList stepGameObjectsList1;
    public StepGameObjectsList stepGameObjectsList2;
    public StepGameObjectsList stepGameObjectsList3;
    public StepGameObjectsList stepGameObjectsList;

    // Start is called before the first frame update
    void Start()
    {
        switch (scenario)
        {
            case 1:
            stepGameObjectsList = stepGameObjectsList1;
            break;
            case 2:
            stepGameObjectsList = stepGameObjectsList2;
            break;
            case 3:
            stepGameObjectsList = stepGameObjectsList3;
            break;
        }

        // foreach (StepGameObject item in stepGameObjectsList[step])
        // {
        //     if (item.setactive() = false)
        //     {
        //         item.setactive(true);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
