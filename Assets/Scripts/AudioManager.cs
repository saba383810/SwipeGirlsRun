using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgmIntroAudioClips = new AudioClip[4];
    [SerializeField] private AudioClip[] bgmLoopAudioClips =new AudioClip[4];
    [SerializeField] private AudioClip[] seAudioClips = new AudioClip[4];
    [SerializeField] private Slider bgmSlider =default;
    [SerializeField] private Slider seSlider = default;
    private AudioSource bgmLoopAudioSource;
    private AudioSource bgmIntroAudioSource;
    private AudioSource seAudioSource;
    private float bgmVolume ;
    private float seVolume ;

    private bool isInit = false;
    // Start is called before the first frame update
    private void Start()
    {

        bgmIntroAudioSource =  transform.GetChild(0).gameObject.AddComponent<AudioSource>();
        bgmLoopAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        seAudioSource = transform.GetChild(1).GetComponent<AudioSource>();
        
        bgmIntroAudioSource.loop = false;
        bgmIntroAudioSource.playOnAwake = false;
        
        bgmLoopAudioSource.loop = true;
        bgmLoopAudioSource.playOnAwake = false;

        seAudioSource.loop = false;
        seAudioSource.playOnAwake = false;

        bgmVolume =  PlayerPrefs.GetFloat("BGMVolume");
        seVolume = PlayerPrefs.GetFloat("SEVolume");

        bgmSlider.value = bgmVolume;
        seSlider.value = seVolume;

        bgmIntroAudioSource.volume = bgmVolume;
        bgmLoopAudioSource.volume = bgmVolume;
        seAudioSource.volume = seVolume;
        
        BGMPlay(0);
        isInit = true;
    }
    

    public void BGMPlay(int bgmNum)
    {
        
        bgmLoopAudioSource.clip = bgmLoopAudioClips[bgmNum];
       
        
        if (bgmIntroAudioClips[bgmNum] != null)
        {
            bgmIntroAudioSource.clip = bgmIntroAudioClips[bgmNum];
            bgmIntroAudioSource.Play();
            Debug.Log("intro");
            bgmLoopAudioSource.PlayScheduled(AudioSettings.dspTime + bgmIntroAudioClips[bgmNum].length);
        }
        else
        {
            bgmLoopAudioSource.Play();
            Debug.Log("再生。");
        }
    }

    public void SePlay(int seNum)
    {
        seAudioSource.PlayOneShot(seAudioClips[seNum]);
    }

    public void ChangeBgmSlider()
    {
        
        bgmIntroAudioSource.volume = bgmSlider.value;
        bgmLoopAudioSource.volume = bgmSlider.value;
        PlayerPrefs.SetFloat("BGMVolume",bgmSlider.value);
    }

    public void ChangeSeSlider()
    {
        if(isInit) SePlay(0);
        seAudioSource.volume = seSlider.value;
        PlayerPrefs.SetFloat("SEVolume",seSlider.value);
    }
}
