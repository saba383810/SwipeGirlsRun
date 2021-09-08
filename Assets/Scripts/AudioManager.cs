using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgmLoopAudioClips =new AudioClip[4];
    [SerializeField] private AudioClip[] seAudioClips = new AudioClip[4];
    [SerializeField] private Slider bgmSlider =default;
    [SerializeField] private Slider seSlider = default;
    private AudioSource bgmLoopAudioSource;
    private AudioSource seAudioSource;
    private float bgmVolume ;
    private float seVolume ;

    private bool isInit = false;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        bgmLoopAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        seAudioSource = transform.GetChild(1).GetComponent<AudioSource>();
        
        
        bgmLoopAudioSource.loop = true;
        bgmLoopAudioSource.playOnAwake = false;

        seAudioSource.loop = false;
        seAudioSource.playOnAwake = false;

        bgmVolume =  PlayerPrefs.GetFloat("BGMVolume");
        seVolume = PlayerPrefs.GetFloat("SEVolume");

        bgmSlider.value = bgmVolume;
        seSlider.value = seVolume;

        bgmLoopAudioSource.volume = bgmVolume;
        seAudioSource.volume = seVolume;

        yield return new WaitForSeconds(0.5f);
        BGMPlay(0);
        if (SceneManager.GetActiveScene().name != "Title")
        {
            yield return new WaitForSeconds(1);
            SePlay(0);
            yield return new WaitForSeconds(1.2f);
            SePlay(1);
        }

        isInit = true;
    }
    

    public void BGMPlay(int bgmNum)
    {
        bgmLoopAudioSource.clip = bgmLoopAudioClips[bgmNum];
        
        bgmLoopAudioSource.Play();
        Debug.Log("再生。");
        
    }

    public void BGMStop()
    {
        bgmLoopAudioSource.Stop();
    }

    public void SePlay(int seNum)
    {
        seAudioSource.PlayOneShot(seAudioClips[seNum]);
    }

    public void ChangeBgmSlider()
    {
        bgmLoopAudioSource.volume = bgmSlider.value;
        PlayerPrefs.SetFloat("BGMVolume",bgmSlider.value);
    }

    public void ChangeSeSlider()
    {
        if(isInit) SePlay(0);
        seAudioSource.volume = seSlider.value;
        PlayerPrefs.SetFloat("SEVolume",seSlider.value);
    }

    public void AudioSet(float bgm,float se)
    {
        bgmSlider.value = bgm;
        seSlider.value = se;
        PlayerPrefs.SetFloat("BGMVolume",bgmSlider.value);
        PlayerPrefs.SetFloat("SEVolume",seSlider.value);
    }
    
    
}
