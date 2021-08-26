using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class BannerAds : MonoBehaviour
    {
        [SerializeField] private string gameId =default; //"GameID"を入力
        public string bannerId = "Banner_iOS";
        public bool testMode = true;
        // Start is called before the first frame update
        private void Start()
        {
            Advertisement.Initialize(gameId, testMode);
            StartCoroutine(ShowBanner());
        
        }

        private IEnumerator ShowBanner()
        {
            while(!Advertisement.isInitialized)
            {
                yield return new WaitForSeconds(0.3f); // 0.3秒後に広告表示
            }
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); //バナーを上部中央にセット
            Advertisement.Banner.Show(bannerId);
        }
    }
}