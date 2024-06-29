using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public GameObject GameOverOverlay;
    public GameObject PausedOverlay;
    public GameObject LevelCompleteOverlay;
    public GameObject BossBattleOverlay;
    public TMP_Text ScoreTxt;
    public TMP_Text DiamondsTxt;

    [SerializeField]
    private GameObject OptionsBar;
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

    public void onOptionsClick()
    {
        OptionsBar.SetActive(!OptionsBar.gameObject.activeSelf); 
    }
  
    public void goHome()
    {
        //Set home as 0
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }


}
