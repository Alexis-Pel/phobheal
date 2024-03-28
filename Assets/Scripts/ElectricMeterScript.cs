using System.Collections;
using System.Collections.Generic;
using UltimateXR.Extensions.System.Collections;
using UnityEngine;

public class ElectricMeterScript : MonoBehaviour
{
    [SerializeField]
    private bool _isOn;

    [SerializeField]
    private List<GameObject> ElectricMeterSwitchs;

    
    // Start is called before the first frame update
    void Start()
    {
        SwitchElectricity();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchElectricity();
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
}
