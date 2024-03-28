using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Light;

    [SerializeField]
    private GameObject Switch;

    [SerializeField]
    private bool _isOn;

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager != null && gameManager._isElectricMeterOn)
        {
            Light.SetActive(true);
        }
        _isOn = true;
        Switch.transform.Rotate(10f, 0f, 0f);
    }

    public void SwitchOnOff(){
        if(_isOn){
            Light.SetActive(false);
            _isOn = false;
            Switch.transform.Rotate(-10f, 0f, 0f);
        }else
        {
            if (gameManager != null && gameManager._isElectricMeterOn){
                Light.SetActive(true);
            }
            _isOn = true;
            Switch.transform.Rotate(10f, 0f, 0f);
        }
    }
    
}
