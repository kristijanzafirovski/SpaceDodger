using UnityEngine;

public class IronSourceInitializer : MonoBehaviour
{
    public string appKey = "1dc3f0e75";

    void Start()
    {
        // Initialize ironSource SDK with specific ad units
        //IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        // Alternatively, you can initialize each ad unit separately
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
        // IronSource.Agent.init(appKey, IronSourceAdUnits.BANNER);
    }

    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    private void OnDisable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent -= SdkInitializationCompletedEvent;
    }

    private void SdkInitializationCompletedEvent()
    {
        // This method will be called when SDK initialization is completed
        // You can start loading ads here
    }
}
