using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerGroup bgmGroup;
    public AudioMixerGroup sfxGroup;
    public AudioSource BgmSound;
    public AudioClip[] BgmList;
    public static SoundManager instance;

    private float defaultBgmVolume = 0.5f; // �⺻ BGM ����
    private float defaultSfxVolume = 0.5f; // �⺻ SFX ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolumeSettings();  // �ʱ� ���� ���� �ҷ�����
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (BgmList.Length > 0)
        {
            BackgroundSound(BgmList[0]);
        }
    }

    public void BgmSoundVolume(float val)
    {
        mixer.SetFloat("BgmSound", Mathf.Log10(val) * 20 - 20);
    }

    public void SfxSoundVolume(float val)
    {
        mixer.SetFloat("SfxSound", Mathf.Log10(val) * 20);
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("BgmSound"))
        {
            float bgmVal = PlayerPrefs.GetFloat("BgmSound");
            BgmSoundVolume(bgmVal);
        }
        else
        {
            BgmSoundVolume(defaultBgmVolume); // �⺻������ �ʱ�ȭ
        }

        if (PlayerPrefs.HasKey("SfxSound"))
        {
            float sfxVal = PlayerPrefs.GetFloat("SfxSound");
            SfxSoundVolume(sfxVal);
        }
        else
        {
            SfxSoundVolume(defaultSfxVolume); // �⺻������ �ʱ�ȭ
        }
    }


    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxGroup;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(go, clip.length);
    }

    public void BackgroundSound(AudioClip clip)
    {
        BgmSound.outputAudioMixerGroup = bgmGroup;
        BgmSound.clip = clip;
        BgmSound.loop = true;
        BgmSound.Play();
    }
}