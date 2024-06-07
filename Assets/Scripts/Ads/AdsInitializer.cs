
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string AndriodGameId;
    [SerializeField] string IOSGameId;
    string GameId;
    [SerializeField] bool TestMode = true;

    [SerializeField] string AndriodInterstitialId;
    [SerializeField] string IOSInterstitialId;
    string InterstitialId;

    [SerializeField] string AndriodRewardedId;
    [SerializeField] string IOSRewardedId;
    string RewardId;

    [SerializeField] string AndriodBannerId;
    [SerializeField] string IOSBannerId;
    string BannerId;

    public void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        //Andriod and IOS have different Ids. This will set the id for the ads based on which platfrom it is run on.
        GameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameId : AndriodGameId;
      
        Advertisement.Initialize(GameId, TestMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        

        //this should be the call that actually starts an ad
        LoadInerstitialAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unity Ads initialization failed.");

    }

    public void LoadInerstitialAd()
    {
        //TODO WHEN CODES ARE READY
        InterstitialId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSInterstitialId : AndriodInterstitialId;
        Advertisement.Load(InterstitialId, this);
        
        //Advertisement.Load("Interstitial_Android", this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error Loading Ad Unit {placementId}: {error.ToString()} - {message}");

    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //Pause Game
        Debug.Log("Ad Start");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //If add was clicked do this
        Debug.Log("Ad Clicked");

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //add is complete do stuff
        Debug.Log("Ad Complete");

        CutScene temp = GameObject.FindObjectOfType<CutScene>();

        if(temp !=null)
        {
            temp.Paused = false;
        }
        
        MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();

        if(musicManager != null) 
        {
            musicManager.PlayMusicAfterAd();
        }

    }
}
