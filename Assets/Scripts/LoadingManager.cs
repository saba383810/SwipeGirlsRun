using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private AsyncOperation async;
    private static readonly int CharaNum = Animator.StringToHash("CharaNum");
    //　シーンロード中に表示するUI画面
    [SerializeField] private CanvasGroup loadUI =default;
    [SerializeField] private Animator runAnim =default;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title") return;
        runAnim.SetInteger(CharaNum,PlayerPrefs.GetInt("PlayerCharacterNum"));
        loadUI.alpha = 1;
        loadUI.gameObject.SetActive(true);
        loadUI.DOFade(0, 0.5f).OnComplete(()=>loadUI.gameObject.SetActive(false));
        
    }


    public void NextScene(string sceneName) 
    {
        runAnim.SetInteger(CharaNum,PlayerPrefs.GetInt("PlayerCharacterNum"));

        loadUI.alpha = 0;
        loadUI.gameObject.SetActive(true);
        loadUI.DOFade(1, 1);
 
        //　コルーチンを開始
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(sceneName);
        
        yield return new WaitUntil(() => async.isDone);
        
    }
}
