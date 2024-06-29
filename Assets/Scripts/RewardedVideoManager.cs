using UnityEngine;

public class RewardedVideoManager : MonoBehaviour
{
    void Start()
    {
        // Registering to Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }

    // Rewarded Video Events
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo) { }
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo) { }
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo) { }
    void RewardedVideoOnAdUnavailable() { }
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo) { 
        if(placement.getRewardName() == "diamonds")
        {
            GameController.Instance.addDiamond(placement.getRewardAmount());
            StoreController.Instance.onAdFinish();
        }
   
    }
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo) { }
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo) { }
    public void ShowRewardedAd()
    {
        // Check if rewarded video is available
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            // Show rewarded video ad
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("Rewarded video is not available.");
        }
    }
}
