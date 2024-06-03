using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioTurnLightScript : MonoBehaviour
{
    public int step = 0;
    public bool test = false;
    public StepGameObjectsList Objects;
    void Update()
    {
        SwitchStep(step, () => { 
            if (test){
                test = false;
                return true;
            }
            return false;
        });
    }
    public void SwitchStep(int step, System.Func<bool> actualise)
    {
        if (actualise())
        {
            Debug.Log(" " + (step < 0 || step >= Objects.Steps.Count) + " " + step + " " + Objects.Steps.Count);
            if (step < 0 || step >= Objects.Steps.Count)
            {
                return;
            }
            
            foreach (var obj in Objects.Steps[step].objects)
            {
                Debug.Log(obj.name+" "+step);
                obj.SetActive(false);
            }

            Debug.Log(" " + (step + 1 < Objects.Steps.Count && Objects.Steps[step + 1].objects != null) + " " + (step + 1));
            if (step + 1 < Objects.Steps.Count && Objects.Steps[step + 1].objects != null)
            {
                foreach (var obj in Objects.Steps[step + 1].objects)
                {
                    obj.SetActive(true);
                }
                this.step++;
            }
        }
    }

}
