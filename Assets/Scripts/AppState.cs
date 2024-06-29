using UnityEngine;

public class AppState : MonoBehaviour
{
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
