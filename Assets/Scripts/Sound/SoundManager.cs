using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource BgmSound;
    public AudioClip[] BgmList;
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 자동으로 첫 번째 BGM을 재생하도록 설정
        if (BgmList.Length > 0)
        {
            BackgroundSound(BgmList[0]);
        }
    }

    public void BgmSoundVolume(float val)
    {
        mixer.SetFloat("BgmSound", Mathf.Log10(val) * 20);
    }

    public void SfxSoundVolume(float val)
    {
        mixer.SetFloat("SfxSound", Mathf.Log10(val) * 20);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(go, clip.length);
    }

    public void BackgroundSound(AudioClip clip)
    {
        BgmSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        BgmSound.clip = clip;
        BgmSound.loop = true;
        BgmSound.Play();
    }
}