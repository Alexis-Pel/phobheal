using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField]
    private Light Light;

    [SerializeField]
    private GameObject Switch;

    public bool _isOn;

    [SerializeField]
    private KenophobiaManager gameManager;

    [SerializeField] private Material lightOnMaterial; // Material for the light on (white)
    [SerializeField] private Material lightOffMaterial; // Material for light off (black)
    [SerializeField] private Renderer lampRenderer; // Render the lamp object
    [SerializeField] private int materialIndexToChange = 0; // Index of the material to modify

    private bool lastIsOnState; // To keep track of the previous state of _isOn


    [SerializeField] private AudioClip switchOnSound;
    [SerializeField] private AudioClip switchOffSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        lastIsOnState = _isOn; // Initialize the last state
        // Check the electric meter status and set the initial state
        /*if (gameManager != null && gameManager._isElectricMeterOn)
        {
            _isOn = true;
            Switch.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
        }
        else
        {
            _isOn = false;
            Switch.transform.localEulerAngles = new Vector3(-10f, 0f, 0f);
        }*/
        UpdateSwitchState();
    }

    void Update()
    {
        // Check if _isOn has changed
        if (_isOn != lastIsOnState)
        {
            UpdateSwitchState();
            lastIsOnState = _isOn;
        }
    }

    private void UpdateSwitchState()
    {
        Light.enabled = _isOn;
        UpdateLampMaterial();
    }

    public void SwitchOnOff(){
        //Debug.Log(_isOn+"   "+gameManager._isElectricMeterOn);
        Vector3 rotation = _isOn ? new Vector3(-10f, 0, 0) : new Vector3(10f, 0, 0);

        if (!Mathf.Approximately(Switch.transform.localEulerAngles.x, rotation.x)){
            Switch.transform.localEulerAngles = rotation;
            
            if(_isOn){
                PlaySound(switchOnSound);
                _isOn = false;

            }else{
                PlaySound(switchOffSound);
                _isOn = true;
            }

             Light.enabled = _isOn;
        }
    }

    private void actualiseLight()
    {
        bool _isOnElec = gameManager._isElectricMeterOn ? true : false;
        if (_isOn && _isOnElec)
        {
            //Light.SetActive(true);
        }
    }

    private void UpdateLampMaterial()
    {
        if (lampRenderer != null)
        {
            Material[] materials = lampRenderer.materials;
            if (materialIndexToChange >= 0 && materialIndexToChange < materials.Length)
            {
                materials[materialIndexToChange] = _isOn ? lightOnMaterial : lightOffMaterial;
                lampRenderer.materials = materials;
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void SetToLight(bool _isOnTemp){
        //Debug.Log(_isOnTemp);
        Vector3 rotation = !_isOnTemp ? new Vector3(-10f, 0, 0) : new Vector3(10f, 0, 0);

        if (!Mathf.Approximately(Switch.transform.localEulerAngles.x, rotation.x)){
            Switch.transform.localEulerAngles = rotation;
            
            if(!_isOnTemp){
                PlaySound(switchOnSound);
                _isOn = false;
            }else{
                PlaySound(switchOffSound);
                _isOn = true;
            }

             Light.enabled = _isOnTemp;
        }
    }
}
