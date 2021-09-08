using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class BannerAds : MonoBehaviour
    {
        private const string IOSGameID = "4279262";
        private const string AndroidGameID = "4279263";
        
        private const string IOSBannerId = "Banner_iOS";
        private const string AndroidBannerId = "Banner_Android";

        public bool testMode = true;
        // Start is called before the first frame update
        private void Start()
        {
#if UNITY_ANDROID
            Advertisement.Initialize(AndroidGameID, testMode);
#elif UNITY_IOS
            Advertisement.Initialize(IOSGameID, testMode);
#endif
            StartCoroutine(ShowBanner());
        }

        private static IEnumerator ShowBanner()
        {
            while(!Advertisement.isInitialized)
            {
                yield return new WaitForSeconds(0.3f); 
            }
            
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); 

#if UNITY_ANDROID
            Advertisement.Banner.Show(AndroidBannerId);
#elif UNITY_IOS
            Advertisement.Banner.Show(IOSBannerId);
#endif
            
        }
    }
}