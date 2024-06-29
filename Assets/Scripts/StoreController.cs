using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public static StoreController Instance { get; private set; }
    [SerializeField]
    private Button Fasty;
    [SerializeField]
    private Button Hasty;
    [SerializeField]
    private Button Swifty;
    [SerializeField]
    private TMP_Text Diamondstxt;
    [SerializeField]
    private GameObject AdScreen;
    int Diamonds;
    /*
    [Space]
    public Image ExplosionLevelSprite;
    public Text ExplosionText;
    public GameObject BlurExplosion;
    public Image FreezeLevelSprite;
    public Text FreezeText;
    public GameObject BlurFreeze;
    public Image SlowDownTimeLevelSprite;
    public Text TimeText;
    public GameObject BlurSlowDownTime;
    public Image ShieldLevelSprite;
    public Text ShieldText;
    public GameObject BlurShield;
    */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        Diamonds = PlayerPrefs.GetInt("diamonds", 0);
        Diamondstxt.text = Diamonds.ToString();
        //make buttons available, if earth default to break -> all not interactible
        switch (PlayerPrefs.GetString("level", "Earth"))
        {
            case "Mars":
                Fasty.interactable = true;
                break;
            case "Jupiter":
                Hasty.interactable = true;
                Fasty.interactable = true;
                break;
            case "Saturn":
                Hasty.interactable = true;
                Fasty.interactable = true;
                Swifty.interactable = true;
                break;
            case "endless":
                Hasty.interactable = true;
                Fasty.interactable = true;
                Swifty.interactable = true;
                break;
            default:
                break;
        }
    }

    public void FastyFunc()
    {
        PlayerPrefs.SetString("rocket", "fasty");
        /*
        SelectedFasty.SetActive(true);
        SelectedHasty.SetActive(false);
        SelectedSwifty.SetActive(false);
        BlurShield.SetActive(false);
        BlurSlowDownTime.SetActive(false);
        BlurFreeze.SetActive(true);
        BlurExplosion.SetActive(true);*/
    }


    public void HastyFunc()
    {
        PlayerPrefs.SetString("rocket", "hasty");
        /*Rocket = 2;
        SelectedFasty.SetActive(false);
        SelectedHasty.SetActive(true);
        SelectedSwifty.SetActive(false);
        BlurShield.SetActive(true);
        BlurSlowDownTime.SetActive(true);
        BlurFreeze.SetActive(false);
        BlurExplosion.SetActive(false);*/
    }

    public void SwiftyFunc()
    {
        PlayerPrefs.SetString("rocket", "swifty");
        /*
        Rocket = 3;
        SelectedFasty.SetActive(false);
        SelectedHasty.SetActive(false);
        SelectedSwifty.SetActive(true);
        BlurShield.SetActive(false);
        BlurSlowDownTime.SetActive(false);
        BlurFreeze.SetActive(false);
        BlurExplosion.SetActive(false);*/
    }

    public void OnExitClick()
    {
        PlayerPrefs.Save();
    }
    //Upgrade Canvas;
    public void OnShieldClick()
    {
        /*
        if (Diamonds >= UMScript.ShieldPrice[UMScript.ShieldStage])
        {
            UMScript.ShieldStage += 1;
            Diamonds -= UMScript.ShieldPrice[UMScript.ShieldStage];
        }
        else
        {
            PopUp.gameObject.SetActive(true);
            PopUpOkButton.gameObject.SetActive(true);
        }*/
    }
    public void OnExplosionClick()
    {/*
        if (Diamonds >= UMScript.ExplosionPrice[UMScript.ExplosionStage])
        {
            UMScript.ExplosionStage += 1;
            Diamonds -= UMScript.ExplosionPrice[UMScript.ExplosionStage];
        }
        else
        {
            PopUp.gameObject.SetActive(true);
            PopUpOkButton.gameObject.SetActive(true);
        }
         */
    }
    public void OnSlowTimeClick()
    {/*
        if (Diamonds >= UMScript.SlowDownTimePrice[UMScript.SlowDownTimeStage])
        {
            UMScript.SlowDownTimeStage += 1;
            Diamonds -= UMScript.SlowDownTimePrice[UMScript.SlowDownTimeStage];
        }
        else
        {
            PopUp.gameObject.SetActive(true);
            PopUpOkButton.gameObject.SetActive(true);
        }
        */
    }
    public void OnFreezeClick()
    {/*
        if (Diamonds >= UMScript.FreezePrice[UMScript.FreezeStage])
        {
            UMScript.FreezeStage += 1;
            Diamonds -= UMScript.FreezePrice[UMScript.FreezeStage];
        }
        else
        {
            PopUp.gameObject.SetActive(true);
            PopUpOkButton.gameObject.SetActive(true);
        }*/
    }
    public void onAdFinish()
    {
        AdScreen.SetActive(true);
    }
    void Update()
    {
        //upgrades
        /*
        switch (UMScript.ExplosionStage)
        {
            case 1:
                ExplosionLevelSprite.rectTransform.sizeDelta = new Vector2(114, 100);
                ExplosionText.text = "$ 100";
                break;
            case 2:
                ExplosionLevelSprite.rectTransform.sizeDelta = new Vector2(228, 100);
                ExplosionText.text = "$ 200";
                break;
            case 3:
                ExplosionLevelSprite.rectTransform.sizeDelta = new Vector2(342, 100);
                ExplosionText.text = "$ 300";
                break;
            case 4:
                ExplosionLevelSprite.rectTransform.sizeDelta = new Vector2(456, 100);
                ExplosionText.text = "$ 400";
                break;
            case 5:
                ExplosionLevelSprite.rectTransform.sizeDelta = new Vector2(570, 100);
                ExplosionText.text = "$ 500";
                break;
        }
        switch (UMScript.FreezeStage)
        {
            case 1:
                FreezeLevelSprite.rectTransform.sizeDelta = new Vector2(114, 100);
                FreezeText.text = "$ 50";
                break;
            case 2:
                FreezeLevelSprite.rectTransform.sizeDelta = new Vector2(228, 100);
                FreezeText.text = "$ 100";
                break;
            case 3:
                FreezeLevelSprite.rectTransform.sizeDelta = new Vector2(342, 100);
                FreezeText.text = "$ 150";
                break;
            case 4:
                FreezeLevelSprite.rectTransform.sizeDelta = new Vector2(456, 100);
                FreezeText.text = "$ 200";
                break;
            case 5:
                FreezeLevelSprite.rectTransform.sizeDelta = new Vector2(570, 100);
                FreezeText.text = "$ 250";
                break;
        }
        switch (UMScript.SlowDownTimeStage)
        {
            case 1:
                SlowDownTimeLevelSprite.rectTransform.sizeDelta = new Vector2(114, 100);
                TimeText.text = "$ 25";
                break;
            case 2:
                SlowDownTimeLevelSprite.rectTransform.sizeDelta = new Vector2(228, 100);
                TimeText.text = "$ 50";
                break;
            case 3:
                SlowDownTimeLevelSprite.rectTransform.sizeDelta = new Vector2(342, 100);
                TimeText.text = "$ 75";
                break;
            case 4:
                SlowDownTimeLevelSprite.rectTransform.sizeDelta = new Vector2(456, 100);
                TimeText.text = "$ 100";
                break;
            case 5:
                SlowDownTimeLevelSprite.rectTransform.sizeDelta = new Vector2(570, 100);
                TimeText.text = "$ 115";
                break;
        }
        switch (UMScript.ShieldStage)
        {
            case 1:
                ShieldLevelSprite.rectTransform.sizeDelta = new Vector2(114, 100);
                ShieldText.text = "$ 10";
                break;
            case 2:
                ShieldLevelSprite.rectTransform.sizeDelta = new Vector2(228, 100);
                ShieldText.text = "$ 20";
                break;
            case 3:
                ShieldLevelSprite.rectTransform.sizeDelta = new Vector2(342, 100);
                ShieldText.text = "$ 30";
                break;
            case 4:
                ShieldLevelSprite.rectTransform.sizeDelta = new Vector2(456, 100);
                ShieldText.text = "$ 40";
                break;
            case 5:
                ShieldLevelSprite.rectTransform.sizeDelta = new Vector2(570, 100);
                ShieldText.text = "$ 50";
                break;
        }

    }*/
    }
}