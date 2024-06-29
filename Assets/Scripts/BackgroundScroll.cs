using UnityEngine.UI;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public static BackgroundScroll Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public float speed = 0;
    void Update()
    {
        gameObject.GetComponent<Image>().material.mainTextureOffset = new Vector2 (0f, (Time.time * speed) % 1);
    }
}
