using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricMeterSwitchScript : MonoBehaviour
{
    [SerializeField]
    private Transform electricMeterSwitchTransform;

    [SerializeField]
    private List<Transform> electricMeterSwitchTransformList;

    [SerializeField]
    private List<GameObject> LightsList;

    [SerializeField]
    private InteractorSwitchElectricMeterScript interactorSwitchElectricMeterScript;

    public bool _isOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       UpdateLights();
    }

   public void UpdateElectricMeter(){
        Vector3 rotation = _isOn ? new Vector3(135f, 90, 0) : new Vector3(0f, 90, 0);
        bool allSwitchesInPosition = true;

        foreach (Transform electricMeterSwitch in electricMeterSwitchTransformList)
        {
            Debug.Log(electricMeterSwitch.transform.localEulerAngles.x+"    "+rotation.x);
            if (!Mathf.Approximately(electricMeterSwitch.transform.localEulerAngles.x, rotation.x))
            {
                allSwitchesInPosition = false;
                break;
            }
        }

        if (allSwitchesInPosition)
        {
            Debug.Log("All switches are in position.");
            electricMeterSwitchTransform.localEulerAngles = new Vector3(0f,90f,0);
            _isOn = true;
            interactorSwitchElectricMeterScript._isOn = true;
        }
        else
        {
            Debug.Log("Not all switches are in position.");
            electricMeterSwitchTransform.localEulerAngles = new Vector3(135f,90f,0);
            _isOn = false;
            interactorSwitchElectricMeterScript._isOn = false;
        }

        foreach (GameObject Light in LightsList){
            Light.SetActive(_isOn);
        }
    }

    public void UpdateLights(){
        foreach (GameObject Light in LightsList){
            Light.SetActive(_isOn);
        }
    }
}
