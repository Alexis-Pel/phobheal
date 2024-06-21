using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricMeterScript : MonoBehaviour
{
    public bool _isOn;

    [SerializeField]
    private List<GameObject> ElectricMeterSwitchs;

    [SerializeField]
    private KenophobiaManager gameManager;

    [SerializeField]
    private List<GameObject> Lights;

    private bool _isStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        SwitchElectricity();
        ActualiseGameManagerIsOn();
    }

    // Update is called once per frame
    void Update()
    {
        //ActualiseGameManagerIsOn();
    }

    private void SwitchElectricity()
    {
        Vector3 rotation = _isOn ? new Vector3(0f, 90, 0) : new Vector3(135f, 90, 0);

        foreach (GameObject electricMeterSwitch in ElectricMeterSwitchs)
        {
            if (!Mathf.Approximately(electricMeterSwitch.transform.localEulerAngles.x, rotation.x))
            {
                electricMeterSwitch.transform.localEulerAngles = rotation;
            }
        }
    }

    private void ActualiseGameManagerIsOn(){
        //gameManager._isElectricMeterOn = _isOn ? true : false;

        foreach (GameObject Light in Lights){
            Light.SetActive(_isOn);
            Light light = Light.GetComponent<Light>();
            light.enabled = _isOn;
        }
    }

    public void ActualiseElectricMeter(bool _isOnLoc){
        _isOn = _isOnLoc;
        SwitchElectricity();
        foreach (GameObject Light in Lights){
            Light.SetActive(_isOnLoc);
        }
    }
}
