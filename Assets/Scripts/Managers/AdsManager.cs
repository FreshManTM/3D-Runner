using UnityEngine;
using UnityEngine.Advertisements;
using PlayerMovement;

public class AdsManager : MonoBehaviour,IUnityAdsInitializationListener ,IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance;

    string androidGameID = "5278622";
    string androidAdID = "Rewarded_Android";

    [SerializeField] PlayerController player;

    private void Awake()
    {
        Instance = this;
        AdsInitialize();
    }

    public void AdsInitialize()
    {
        Advertisement.Initialize(androidGameID, true, this);
    }

    void LoadAd()
    {
        Advertisement.Load(androidAdID, this);
    }
    
    public void ShowAd()
    {
        Debug.Log("Ad Showing!");
        Advertisement.Show(androidAdID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("Ad loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Failed to load: " + error);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Failed to show: " + error);

    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ads start");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ads show Click");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Get reward");
        StateMachine stateMachine = new StateMachine();
        stateMachine.ChangeState(new GameState(), null);
        player.ResumePosition();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Initialization complete");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unity Initialization failed: " + error);
    }
}
