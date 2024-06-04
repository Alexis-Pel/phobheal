using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KenophobiaManager : MonoBehaviour
{
    public bool _isElectricMeterOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int SwitchStep(int step,StepGameObjectsList objects, System.Func<bool> actualise)
    {
        if (actualise())
        {
            Debug.Log(" " + (step < 0 || step >= objects.Steps.Count) + " " + step + " " + objects.Steps.Count);
            if (step < 0 || step >= objects.Steps.Count)
            {
                return step;
            }
            
            foreach (var obj in objects.Steps[step].objects)
            {
                Debug.Log(obj.name+" "+step);
                obj.SetActive(false);
            }

            Debug.Log(" " + (step + 1 < objects.Steps.Count && objects.Steps[step + 1].objects != null) + " " + (step + 1));
            if (step + 1 < objects.Steps.Count && objects.Steps[step + 1].objects != null)
            {
                foreach (var obj in objects.Steps[step + 1].objects)
                {
                    obj.SetActive(true);
                }
                
            }
            return step++;
        }
        return step;
    }
}
