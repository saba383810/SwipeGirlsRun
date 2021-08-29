using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;
using UnityEngine.UI;
using ShowResult = UnityEngine.Monetization.ShowResult;

namespace Ads
{

    [RequireComponent(typeof(Button))]
    public class RewardAdsButton : MonoBehaviour
    {

        [SerializeField] private TItleButtonManager titleButtonManager =default;
        [SerializeField] private CanvasGroup rewardResultWindow =default;
        [SerializeField] private Text resultText = default;
        
        private Button adButton;
        private int characterNum;

# if UNITY_IOS
        private string gameId = "4279262";
        private  string placementId = "Rewarded_iOS";
# elif UNITY_ANDROID
        private  string placementId = "Rewarded_Android";
        private string gameId = "4279263";
# endif

        void Start()
        {
            adButton = GetComponent<Button>();
            if (adButton)
            {
                
                adButton.onClick.AddListener(ShowAd);
            }

            if (Monetization.isSupported)
            {
                Monetization.Initialize(gameId, true);
            }
        }

        void Update()
        {
            if (!adButton) return;
            characterNum = PlayerPrefs.GetInt("PlayerCharacterNum");
            adButton.interactable = Monetization.IsReady(placementId);
        }

        void ShowAd()
        {
            var options = new ShowAdCallbacks();
            options.finishCallback = HandleShowResult;
            var ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
            ad.Show(options);
        }

        void HandleShowResult(ShowResult result)
        {
            rewardResultWindow.alpha = 0;
            rewardResultWindow.gameObject.SetActive(true);
            rewardResultWindow.DOFade(1, 1);
            if (result == ShowResult.Finished)
            {
                switch (characterNum)
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
            }
            else if (result == ShowResult.Skipped)
            {
                resultText.text = "広告がスキップされました。";
                Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
            }
            else if (result == ShowResult.Failed)
            {
                resultText.text = "広告を表示することができませんでした";
                Debug.LogError("Video failed to show");
            }
        }

        public void RewardResultWindowExit()
        {
            rewardResultWindow.DOFade(0, 1).OnComplete(() => rewardResultWindow.gameObject.SetActive(false));
            titleButtonManager.CharaRewardCheck(characterNum);
        }
    }
}
