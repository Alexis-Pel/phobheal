using UnityEngine;

public class UnderwaterEffect : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private Material skyboxSurface;
    [SerializeField] private Material skyboxUnderwater;
    [SerializeField] private Material skyboxDeepUnderwater;

    [SerializeField] private float maxDepthSunlight;

    private float maxExposure;
    private float waterNivelY = 0.35f;

    private void Start()
    {
        maxExposure = 1;
    }
    private void Update()
    {
        float currentDepth = mainCamera.transform.position.y;
        RenderSettings.fog = currentDepth < waterNivelY;

        UpdateSkybox(currentDepth);
    }

    private void UpdateSkybox(float currentDepth)
    {
        if (currentDepth < waterNivelY)
        {
            float percentageDepth = Mathf.Abs(currentDepth) / Mathf.Abs(maxDepthSunlight);
            // Need to re-Instanciate another Materiel, or else it will rewrite the data on the reference one
            Material dummySkybox = Instantiate(skyboxUnderwater);
            dummySkybox.Lerp(skyboxUnderwater, skyboxDeepUnderwater, percentageDepth);
            RenderSettings.skybox = dummySkybox;
        } else
        {
            RenderSettings.skybox = skyboxSurface;
        }
    }
}
