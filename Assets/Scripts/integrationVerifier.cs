using UnityEngine;

public class integrationVerifier : MonoBehaviour
{
    void Start()
    {
        // Verify ironSource SDK integration
        IronSource.Agent.validateIntegration();
    }
}
