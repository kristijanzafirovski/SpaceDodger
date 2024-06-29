using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField]
    private int CurrScore = 0;
    private int diamondAmount;
    [SerializeField]
    private GameObject player,Boss;
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private AudioSource BossBattleSound;
    private int ScoreToAchieve;
    // Start is called before the first frame update
    private void Start()
    {
        setupGame();
    }
    private void setupGame()
    {
        //Get stats
        diamondAmount = PlayerPrefs.GetInt("diamonds", 0);
        string level = PlayerPrefs.GetString("level", "Earth");
        string rocket = PlayerPrefs.GetString("rocket", "default");
        switch (level)
        {
            case "Earth":
                ScoreToAchieve = 1500;
                break;
            case "Mars":
                CurrScore = 1500;
                ScoreToAchieve = 2500;
                break;
            case "Jupiter":
                CurrScore = 2500;
                ScoreToAchieve = 3500;
                break;
            case "Saturn":
                CurrScore = 3500;
                ScoreToAchieve = 5000;
                break;
            case "endless":
                ScoreToAchieve = 0;
                break;
        }
        //set skins, planet, UI
        if (level != "endless")
        {
            planet.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + level); //load the current planet, if endless mode load earth
        }
        else planet.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Earth");
        player.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + rocket);
        //Set diamonds, make sure boss is disabled
        UIController.Instance.DiamondsTxt.text = diamondAmount.ToString();
        Boss.SetActive(false);
        UIController.Instance.BossBattleOverlay.SetActive(false);
        //Ensure timescale is 1
        Time.timeScale = 1;
    }
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
    public void DelayGameStart()
    {
        //Start spawner
        Spawner.Instance.StartSpawning = true;
        StartCoroutine(ScoreCounter());
    }
    IEnumerator ScoreCounter()
    {
        while (true)
        {
            if (CurrScore >= ScoreToAchieve && ScoreToAchieve!= 0)
            {
                StartBossBattle();
                break;
            }
            else
            {
                CurrScore += 1;
                UIController.Instance.ScoreTxt.text = CurrScore.ToString();
                yield return new WaitForSeconds(0.2f);
            }
        }
        
    }
    public IEnumerator GameOver()
    {
        StopCoroutine(ScoreCounter()); // <- restart on reseting game or continue after ad
        //delay to let animation play
        yield return new WaitForSeconds(1f);
        //Stop bckg
        BackgroundScroll.Instance.speed = 0;
        //Stop spawner and counter
        Spawner.Instance.StartSpawning = false;
        //Prevent movement of rocket
        Time.timeScale = 0;
        //Save diamonds and score (if > than HS)
        PlayerPrefs.SetInt("diamonds", diamondAmount);
        if(CurrScore > PlayerPrefs.GetInt("HS",0))
        {
            PlayerPrefs.SetInt("HS", CurrScore);
        }
        PlayerPrefs.Save();
        //Show GameOverScreen
        UIController.Instance.GameOverOverlay.SetActive(true);
    }
    public void addDiamond(int amount)
    {
        diamondAmount += amount;
        UIController.Instance.DiamondsTxt.text = diamondAmount.ToString();
    }
    public void StartBossBattle()
    {
        StopCoroutine(ScoreCounter());
        Boss.SetActive(true);
        BossBattleSound.Play();
        UIController.Instance.BossBattleOverlay.SetActive(true);   
        Spawner.Instance.StartSpawning = false;
    }
    public void OnBossBattleComplete()
    {
        Boss.SetActive(false);
        BossBattleSound.Stop();
        LevelComplete();
    }
    private void LevelComplete()
    {
        StopCoroutine (ScoreCounter());
        UIController.Instance.LevelCompleteOverlay.SetActive(true);
        UIController.Instance.BossBattleOverlay.SetActive (false);
        pauseGame();
        //save score and diamonds
        switch(ScoreToAchieve)
        {
            case 1500:
                PlayerPrefs.SetString("level", "Mars");
                break;
            case 2500:
                PlayerPrefs.SetString("level", "Jupiter");
                break;
            case 3500:
                PlayerPrefs.SetString("level", "Saturn");
                break;
            default:
                PlayerPrefs.SetString("level", "endless");
                break;
        }
        PlayerPrefs.SetInt("diamonds", diamondAmount);
        PlayerPrefs.Save();
    }
    public void ContinueToNextLevel()
    {
        setupGame();
        ResetGame();

    }
    public void pauseGame()
    {
        StopCoroutine(ScoreCounter());
        BackgroundScroll.Instance.speed = 0;
        Spawner.Instance.StartSpawning = false;
        Time.timeScale = 0;
    }
    public void resumeGame()
    {
        StartCoroutine(ScoreCounter());
        BackgroundScroll.Instance.speed = 1;
        Spawner.Instance.StartSpawning = true;
        Time.timeScale = 1;
    }
    public void ResetGame()
    {
        Debug.Log("resetting");
        Spawner.Instance.StartSpawning = true;
        CurrScore = 0;
        UIController.Instance.ScoreTxt.text = "0";
        BackgroundScroll.Instance.speed = 1;
        Time.timeScale = 1;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        StartCoroutine(ScoreCounter()); 

    }


 
}
