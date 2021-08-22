using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgmIntroAudioClips = new AudioClip[4];
    [SerializeField] private AudioClip[] bgmLoopAudioClips =new AudioClip[4];
    [SerializeField] private AudioClip[] seAudioClips = new AudioClip[4];
    private AudioSource bgmLoopAudioSource;
    private AudioSource bgmIntroAudioSource;
    private AudioSource seAudioSource;    
    
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
}
