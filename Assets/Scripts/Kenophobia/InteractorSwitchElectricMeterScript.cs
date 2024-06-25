using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorSwitchElectricMeterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject SwitchElementMeter;

    public bool _isOn;
    public bool _isSwitchController = false;

    void Start() {
        SwitchElementMeter = transform.gameObject;
    }
    public void InteractorSwitchElectricMeterFun(){
        Debug.Log(_isOn + "   " + _isSwitchController);
        if(!_isOn || _isSwitchController){
            Debug.Log("coucou BG");
            Vector3 rotation = !_isOn ? new Vector3(0f, 90, 0) : new Vector3(135f, 90, 0);
            if (!Mathf.Approximately(SwitchElementMeter.transform.localEulerAngles.x, rotation.x))
            {
                Debug.Log("coucou Ma Bite");
                SwitchElementMeter.transform.localEulerAngles = rotation;
                _isOn = !_isOn;
            }
        }
    }
}
