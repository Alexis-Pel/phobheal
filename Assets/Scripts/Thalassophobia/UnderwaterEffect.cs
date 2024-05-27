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

    public float WaterNivelY { get; private set; }

    private void Start()
    {
        WaterNivelY = transform.position.y;
    }

    public bool IsUnderWater() => mainCamera.transform.position.y <= WaterNivelY;

    private void Update()
    {
        bool _isUnderWater = IsUnderWater();
        RenderSettings.fog = _isUnderWater;
        // Rotate the water plane. It's not seen on the other side, and to preserve performance, we define only 1 water surface
        transform.rotation = Quaternion.AngleAxis(_isUnderWater ? 180 : 0, Vector3.back);

        UpdateSkybox();
    }

    private void UpdateSkybox()
    {
        if (IsUnderWater())
        {
            float percentageDepth = Mathf.Abs(mainCamera.transform.position.y) / Mathf.Abs(maxDepthSunlight);
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
