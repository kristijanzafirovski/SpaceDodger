using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRocket : MonoBehaviour
{
    
    public void StartGameTrigger()
    {
        GameController.Instance.DelayGameStart();
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
