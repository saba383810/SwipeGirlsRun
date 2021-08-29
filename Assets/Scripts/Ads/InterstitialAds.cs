using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class InterstitialAds : MonoBehaviour
    {
        
        private const string IOSGameID = "4279262";
        private const string AndroidGameID = "4279263";
        
        private const string IOSInterstitialId = "Interstitial_iOS";
        private const string AndroidInterstitialId = "Interstitial_Android";
        public bool testMode = true;
        private void Start()
        {
#if UNITY_ANDROID
            Advertisement.Initialize(AndroidGameID, testMode);
#elif UNITY_IOS
            Advertisement.Initialize(IOSGameID, testMode);
#endif
        }

        public static IEnumerator ShowInterstitial()
        {
            yield return new WaitForSeconds(0.2f);
            while(!Advertisement.isInitialized)
            {
                yield return new WaitForSeconds(0.3f); 
            }
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            
#if UNITY_ANDROID
            Advertisement.Show(AndroidInterstitialId);
#elif UNITY_IOS
            Advertisement.Show(IOSInterstitialId);
#endif
        }
    }
}
