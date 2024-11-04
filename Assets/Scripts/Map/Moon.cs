using UnityEngine;

public class Moon : MonoBehaviour
{
    public Light directionalLight; // Directional Light
    public float dayDuration = 60f; // �Ϸ��� ���� �ð� (��)
    private float time;

    // Fog ����
    public Color fogColor = Color.gray; // Fog ����
    public float maxFogDensity = 0.1f; // �ִ� Fog �е�
    private float targetFogDensity = 0f; // ��ǥ Fog �е�
    private float currentFogDensity = 0f; // ���� Fog �е�

    // Fog�� ��ȭ �ӵ� ����
    public float fogTransitionSpeed = 0.05f; // Fog ��ȭ �ӵ�

    void Start()
    {
        // �ʱ� Fog ����
        RenderSettings.fog = true; // Fog Ȱ��ȭ
        RenderSettings.fogColor = fogColor; // Fog ���� ����
        RenderSettings.fogDensity = 0f; // �ʱ� Fog �е�

        // ������ ����
        time = 0f; // ���� ����
    }

    void Update()
    {
        // �ð� ����
        time += Time.deltaTime;

        // �Ϸ縦 0-1�� ����ȭ
        float normalizedTime = (time % dayDuration) / dayDuration;

        // Directional Light�� ���� ����
        float angle = normalizedTime * 360f - 90f; // -90���� �����Ͽ� �������� ����
        directionalLight.transform.rotation = Quaternion.Euler(angle, 0, 0);

        // Fog �е� ����
        if (normalizedTime < 0.5f) // ��
        {
            targetFogDensity = 0f; // �� ���� Fog�� ����
        }
        else // ��
        {
            targetFogDensity = maxFogDensity; // �� ���� Fog�� �ִ� �е��� ����
        }

        // ���� Fog �е��� ��ǥ �е��� �ε巴�� ��ȭ
        currentFogDensity = Mathf.Lerp(currentFogDensity, targetFogDensity, fogTransitionSpeed);
        RenderSettings.fogDensity = currentFogDensity;
    }
}
