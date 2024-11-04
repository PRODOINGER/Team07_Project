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

    private float defaultBgmVolume = 0.5f; // 기본 BGM 볼륨
    private float defaultSfxVolume = 0.5f; // 기본 SFX 볼륨

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolumeSettings();  // 초기 볼륨 설정 불러오기
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
            BgmSoundVolume(defaultBgmVolume); // 기본값으로 초기화
        }

        if (PlayerPrefs.HasKey("SfxSound"))
        {
            float sfxVal = PlayerPrefs.GetFloat("SfxSound");
            SfxSoundVolume(sfxVal);
        }
        else
        {
            SfxSoundVolume(defaultSfxVolume); // 기본값으로 초기화
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