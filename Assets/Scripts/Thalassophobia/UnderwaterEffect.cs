using UnityEngine;

public class UnderwaterEffect : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private Material skyboxSurface;
    [SerializeField] private Material skyboxUnderwater;
    [SerializeField] private Material skyboxDeepUnderwater;

    [SerializeField] private Color startDepthFogColor;
    [SerializeField] private Color endDepthFogColor;

    [SerializeField] private float maxDepthSunlight;

    private readonly float _waterNivelY = 0.35f;

    private void Update()
    {
        float currentDepth = mainCamera.transform.position.y;
        RenderSettings.fog = currentDepth < _waterNivelY;

        UpdateSkybox(currentDepth);
    }

    private void UpdateSkybox(float currentDepth)
    {
        if (currentDepth < _waterNivelY)
        {
            float percentageDepth = Mathf.Abs(currentDepth) / Mathf.Abs(maxDepthSunlight);
            // Need to re-Instanciate another Materiel, or else it will rewrite the data on the reference one
            Material dummySkybox = Instantiate(skyboxUnderwater);
            dummySkybox.Lerp(skyboxUnderwater, skyboxDeepUnderwater, percentageDepth);
            RenderSettings.skybox = dummySkybox;
            RenderSettings.fogColor = Color.Lerp(startDepthFogColor, endDepthFogColor, percentageDepth);
        } else
        {
            RenderSettings.skybox = skyboxSurface;
        }
    }
}
