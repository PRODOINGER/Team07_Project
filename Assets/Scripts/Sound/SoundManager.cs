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
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < BgmList.Length; i++)
        {
            BackguoundSound(BgmList[i]);
        }
    }

    public void BgmSoundVoiume(float val)
    {
        mixer.SetFloat("BgmSound", Mathf.Log10(val) * 20);
    }
    public void SfxSoundVoiume(float val)
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

    public void BackguoundSound(AudioClip clip)
    {
        BgmSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        BgmSound.clip = clip;
        BgmSound.loop = true;
        BgmSound.volume = 0.1f;
        BgmSound.Play();
    }
}