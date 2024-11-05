using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; 
    public float dayDuration = 60f; 
    private float time;

    public Color fogColor = Color.gray; 
    public float maxFogDensity = 0.1f; 
    private float targetFogDensity = 0f; 
    private float currentFogDensity = 0f; 

    public float fogTransitionSpeed = 0.05f; 

    void Start()
    {
        RenderSettings.fog = true; 
        RenderSettings.fogColor = fogColor; 
        RenderSettings.fogDensity = 0f; 
    }

    void Update()
    {
        time += Time.deltaTime;

        float normalizedTime = (time % dayDuration) / dayDuration;

        float angle = normalizedTime * 360f; 
        directionalLight.transform.rotation = Quaternion.Euler(angle, 0, 0); 

        if (normalizedTime < 0.5f) 
        {
            targetFogDensity = 0f; 
        }
        else // นใ
        {
            targetFogDensity = maxFogDensity; 
        }

        currentFogDensity = Mathf.Lerp(currentFogDensity, targetFogDensity, fogTransitionSpeed);
        RenderSettings.fogDensity = currentFogDensity;
    }
}
