using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStep : MonoBehaviour
{
    public Button buttonToClick;
    public ScenarioController scenarioController;
    [SerializeField] private TMP_Text phraseTutoText;
    [SerializeField] private string phraseScenario;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(phraseTutoText);
        phraseTutoText.text = phraseScenario;
        Debug.Log("Bouton a cliqué ???");

        // Ajout d'un listener au bouton
        if (buttonToClick != null)
        {
            buttonToClick.onClick.AddListener(OnButtonClicked);
            Debug.Log("Bouton AddListener");
        }
        else
        {
            Debug.LogError("ButtonStep: Aucun bouton assigné.");
        }
        
    }

    private void OnButtonClicked()
    {
        Debug.Log("Bouton cliqué !");
        scenarioController.NextStep();
        buttonToClick.onClick.RemoveListener(OnButtonClicked);
    }
}
