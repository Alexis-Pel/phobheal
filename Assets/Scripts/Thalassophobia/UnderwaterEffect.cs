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

    private float _waterNivelY;

    private void Start()
    {
        _waterNivelY = transform.position.y;
    }

    private void Update()
    {
        float currentDepth = mainCamera.transform.position.y;
        RenderSettings.fog = currentDepth < _waterNivelY;
        // Rotate the water plane. It's not seen on the other side, and to preserve performance, we define only 1 water surface
        transform.rotation = Quaternion.AngleAxis(currentDepth < _waterNivelY ? 180 : 0, Vector3.back);

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
