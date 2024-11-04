using UnityEngine;

public class Moon : MonoBehaviour
{
    public Light directionalLight; // Directional Light
    public float dayDuration = 60f; // 하루의 지속 시간 (초)
    private float time;

    // Fog 설정
    public Color fogColor = Color.gray; // Fog 색상
    public float maxFogDensity = 0.1f; // 최대 Fog 밀도
    private float targetFogDensity = 0f; // 목표 Fog 밀도
    private float currentFogDensity = 0f; // 현재 Fog 밀도

    // Fog의 변화 속도 조절
    public float fogTransitionSpeed = 0.05f; // Fog 변화 속도

    void Start()
    {
        // 초기 Fog 설정
        RenderSettings.fog = true; // Fog 활성화
        RenderSettings.fogColor = fogColor; // Fog 색상 설정
        RenderSettings.fogDensity = 0f; // 초기 Fog 밀도

        // 낮부터 시작
        time = 0f; // 시작 시점
    }

    void Update()
    {
        // 시간 증가
        time += Time.deltaTime;

        // 하루를 0-1로 정규화
        float normalizedTime = (time % dayDuration) / dayDuration;

        // Directional Light의 각도 설정
        float angle = normalizedTime * 360f - 90f; // -90으로 조정하여 수평으로 시작
        directionalLight.transform.rotation = Quaternion.Euler(angle, 0, 0);

        // Fog 밀도 설정
        if (normalizedTime < 0.5f) // 낮
        {
            targetFogDensity = 0f; // 낮 동안 Fog를 제거
        }
        else // 밤
        {
            targetFogDensity = maxFogDensity; // 밤 동안 Fog를 최대 밀도로 설정
        }

        // 현재 Fog 밀도를 목표 밀도로 부드럽게 변화
        currentFogDensity = Mathf.Lerp(currentFogDensity, targetFogDensity, fogTransitionSpeed);
        RenderSettings.fogDensity = currentFogDensity;
    }
}
