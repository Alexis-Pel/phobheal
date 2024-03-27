using UltimateXR.Extensions.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Cage : MonoBehaviour
{
    [SerializeField] private float offsetHeight;
    private float modifier;
    // Update is called once per frame
    void Update()
    {
        transform.SetPositionY(transform.position.y + GetOffset());
    }

    public void SetModifier(float modifier)
    {
        this.modifier = modifier;
    }

    public float GetOffset()
    {
        return offsetHeight * (modifier - 0.5f) * 2f;
    }
}
