using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{    
    private GameManager GM;
    private CameraFollower cam;
    private int AdCounter = 0;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("AdTry")) PlayerPrefs.SetInt("AdTry", 0);
        AdCounter = PlayerPrefs.GetInt("AdTry");
    }

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollower>();
        Advertisement.Initialize("4042193", false);
        StartCoroutine(ShowBanner());
    }

    public void ShowAd(string AdType)
    {
        if (AdType == "video")
        {
            AdCounter++;
            if (AdCounter == 3)
            {
                Advertisement.Show("Android_Interstitial");
                AdCounter = 0;
            }
            else
            {
                GM.Restart();
                cam.GameRestart();
            }
            PlayerPrefs.SetInt("AdTry", AdCounter);            
        }
        else
        {
            if (Advertisement.isInitialized) Advertisement.Show(AdType);
            else Advertisement.Show("Android_Interstitial");
        }        
    }

    IEnumerator ShowBanner()
    {
        while (!Advertisement.Banner.isLoaded)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("Android_Banner");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        GM.Restart();
        cam.GameRestart();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        GM.Restart();
        cam.GameRestart();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        GM.Respawn();
        cam.GameRevive();
    }
}
