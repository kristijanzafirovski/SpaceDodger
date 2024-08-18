
# ПРОЕКТНА ЗАДАЧА ПО ВИЗУЕЛНО ПРОГРАМИРАЊЕ - Space Dodger
ДОКУМЕНТАЦИЈА

Проектот Space Dodger е игра во аркаден стил, развиена со C# и Unity, каде што се движи ракета низ бескрајна вселенска средина. Целта е да се избегнат пречки (астероиди), да се собираат дијаманти и да се напредува низ различни „планети“ (нивоа) кои се потешки. Овој документ ја прикажува моменталната механика на играта, како и објаснува дел од главниот код.

# Изглед на играта
#### Слика 1 - Почетен екран

![App Screenshot]()

#### Слика 2 - Тек на игра

![App Screenshot](https://play-lh.googleusercontent.com/TeFR1BomAjuMH5LOF-xX572oklZ0cHDYAxsp60agNOS0BAMS6SJM_U7IK4TTMJ9oe5M=w2560-h1440-rw)

#### Слика 3 - Продавница

![App Screenshot](https://play-lh.googleusercontent.com/MXBuKpWZ55_rTzZ_g65jcI7jSdDVdWLfmVysEvrnT4k1loBrGNiUFqMBY5eUQ4VjBuc=w2560-h1440-rw)

# Контрола
Играта се состои од повеќе мали скрипти кои прават различни делови, но најзначајни се скриптата за контрола на ракетата и скриптата за контрола на игра.

#### RocketController.cs
```C#
На почетокот на кодот се дефинирани сите променливи како позиција, промена на положба,
како и референци кон source-files и други објекти
public class RocketController : MonoBehaviour
  {
    Vector2 touchpos;
    float deltax,deltay;
    Rigidbody2D rb;
    private AudioSource audioSource;
    [SerializeField] private AudioSource LaserSfx;
    [SerializeField] private GameObject laser, gun, explosion;
    //Boss Battle
    private int health = 100;
    [SerializeField] private Slider healthBar;
```
ја користиме карактеристиката на void Update дека е функција што се повикува на секоја слика(frame) од играта, и внатре проверуваме дали има допир на екранот, по што се калкулира неговата позиција во сцената и со switch во Began карактеристиката што се повикува во моментот кога е направен допирот се зачувуваат делта дистанците. Moved карактеристиката е повикана се додека го движиме прстот по екранот, со што користејки го RigidBody на ракетата и функцијата MovePosition ја поместува ракетата соодветно, додека Ended се повикува во моментот кога прстот ќе прекине да допира на ектранот, и само се замрзнува поместувањето со нулирање на забрзувањето.
```C#
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchpos = Camera.main.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        deltax = touchpos.x - transform.position.x;
                        deltay = touchpos.y - transform.position.y;
                        break;

                    case TouchPhase.Moved:
                        rb.MovePosition(new Vector2(touchpos.x - deltax, touchpos.y - deltay));
                        break;

                    case TouchPhase.Ended:
                        rb.velocity = Vector2.zero;
                        break;
                }
            }
    }
```
ОnCollisionEnter2D e функција на Unity која проверува дали објектот направил колизија со други објекти. Во оваа игра е битно да знаеме дали се судрил со метеор, дијамант или ласер и соодветно да се постапи. 
функцијата Shoot се повикува со допир на копчето за пукање во играта, и прави нов објект кој има своја скрипта со која проверува за колизии.
```C#
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Meteor")
        {
            explosion.SetActive(true);
            audioSource.Stop();
            StartCoroutine(GameController.Instance.GameOver());
        }else if(collision.collider.gameObject.tag == "Diamond")
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/collect");
            audioSource.PlayOneShot(clip);
            GameController.Instance.addDiamond(1);
        }else if(collision.collider.gameObject.tag == "UFOLaser")
        {
            health -= 5;
            healthBar.value = health;
            if(health <= 0)
            {
                explosion.SetActive(true);
                audioSource.Stop();
                StartCoroutine(GameController.Instance.GameOver());
            }
        }
    }
    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
    public void Shoot()
    {
        LaserSfx.Play();
        GameObject instantiated = Instantiate(laser, gun.transform.position, Quaternion.Euler(Vector3.up));
    }
}
```
#### GameController.cs
Најбитниот дел од играта и најголем дел од логиката се наоѓа тука. Се зачувани променливи како моменталниот број на дијаманти, поените, референци кон други објекти.
```C#
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
```
setupGame се повикува при секое рестартирање на играта и е добар начин за динамички да се менува левелот и ракетата без да се прават посебни сцени. Со PlayerPrefs се читаат серијализирани податоци за видот на ракетата, бројот на дијаманти и кој левел е моментално играчот и соодветно во switch се променува бројот на поени потребен за следниот левел. Останатиот дел од функцијата е објаснет со коментари во кодот, 
```C#
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
        //Ensure timescale is 1 so game starts
        Time.timeScale = 1;
    }
```
Awake функцијата се повикува уште пред првиот frame update и затоа е погодна за да се направави static инстанца од скриптата за да може да се повикува по референца од други скрипти во нивниот Start дел кој е повикан отпосле. Ова парче код може да се забележи низ многу други скрипти. Повикувањето на инстанцата иде по терк ИмеНаСкрипта.Instance.ПотребенДел или во конкретен пример GameController.Instance.(функција/варијабла).
```C#
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
```
IEnumerator се видови на функции кои кога се повикуваат со StartCoroutine функцијата како над текстов, се извршуваат рекурзивно како тајмери со yield return што чека одредено време пред да го изврши тоа парче код повторно. Погодни се како во овој случај кога треба цело време да се бројат поените, се додека не се стигне до потребните поени.
```C#
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
```






