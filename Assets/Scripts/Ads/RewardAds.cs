using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardAds : MonoBehaviour
{
    
    [SerializeField] private TItleButtonManager titleButtonManager =default;
    [SerializeField] private CanvasGroup rewardResultWindow =default;
    [SerializeField] private Text resultText = default;
    private bool IsTestMode = false;

# if UNITY_IOS
    private string gameId = "4279262";
    private  string placementId = "Rewarded_iOS";
# elif UNITY_ANDROID
        private  string placementId = "Rewarded_Android";
        private string gameId = "4279263";
# endif
    
    void Awake()
    {
        Advertisement.Initialize(gameId, IsTestMode);
    }

    public void ShowAds()
    {
        // 初期化に失敗していたら、再生しない
        if (!Advertisement.isInitialized) return;

        if (Advertisement.IsReady(placementId))
        {
            // 広告表示後のオプション設定
            var options = new ShowOptions
            {
                resultCallback = (result) =>
                {
                    rewardResultWindow.alpha = 0;
                    rewardResultWindow.gameObject.SetActive(true);
                    rewardResultWindow.DOFade(1, 1);

                    switch (result)
                    {
                        case ShowResult.Finished: // 最後まで正常に再生
                            Debug.Log("The Ads was successfully shown.");
                            
                            
                            switch (PlayerPrefs.GetInt("PlayerCharacterNum"))
                            {
                                case 0 :
                                    PlayerPrefs.SetInt("MisakiGet",1);
                                    break;
                                case 1 :
                                    PlayerPrefs.SetInt("TokoGet",1);
                                    break;
                                case 2 :
                                    PlayerPrefs.SetInt("KohakuGet",1);
                                    break;
                                case 3 :
                                    PlayerPrefs.SetInt("YukoGet",1);
                                    break;
                        
                            }
                            titleButtonManager.RewardWindowExit();
                            resultText.text = "キャラクターを解放しました！";
                            
                            break;
                        case ShowResult.Skipped: // 途中でスキップされた
                            Debug.Log("The Ads was skipped.");
                            resultText.text = "広告がスキップされました。";
                            break;
                        case ShowResult.Failed: // 再生に失敗した
                            Debug.Log("The Ads failed to be shown.");
                            resultText.text = "広告を表示することができませんでした";
                            break;
                    }
                }
            };

            Advertisement.Show(placementId, options);
        }
    }
    public void RewardResultWindowExit()
    {
        rewardResultWindow.DOFade(0, 1).OnComplete(() => rewardResultWindow.gameObject.SetActive(false));
        titleButtonManager.CharaRewardCheck(PlayerPrefs.GetInt("PlayerCharacterNum"));
    }
}
