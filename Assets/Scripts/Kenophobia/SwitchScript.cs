using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField]
    private Light Light;

    [SerializeField]
    private GameObject Switch;

    [SerializeField]
    private bool _isOn;

    [SerializeField]
    private KenophobiaManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager != null && gameManager._isElectricMeterOn)
        {
            _isOn = true;
            Switch.transform.Rotate(10f, 0f, 0f);
        }else{
            _isOn = false;
            Switch.transform.Rotate(-10f, 0f, 0f);
        }
        Light.enabled = _isOn;
    }

    public void SwitchOnOff(){
        Debug.Log(_isOn+"   "+gameManager._isElectricMeterOn);
        Vector3 rotation = _isOn ? new Vector3(-10f, 0, 0) : new Vector3(10f, 0, 0);
        if (!Mathf.Approximately(Switch.transform.localEulerAngles.x, rotation.x)){
            Switch.transform.localEulerAngles = rotation;
            if(_isOn){
                _isOn = false;
            }else{
                _isOn = true;
            }
             Light.enabled = _isOn;
        }
    }

    private void actualiseLight(){
        bool _isOnElec = gameManager._isElectricMeterOn ? true : false;
        if (_isOn && _isOnElec){
            //Light.SetActive(true);
        }
    }
    
}
