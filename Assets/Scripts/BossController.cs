using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject Gun1, Gun2, Gun3, Gun4;
    [SerializeField] private float TimeBetweenShots = 0.2f, CoolDownPeriod = 1f;
    [SerializeField] private Slider BossHealthSlider;
    [SerializeField] private GameObject Laser;
    [SerializeField] private AudioSource LaserSfx;
    private int DamageDealt;
    private int BossHealth = 100;

    Vector3 MovePosition;
    Vector2 screenHalfSizeWorldUnits;
    private float MoveTime = 2f;
    private bool AttackFinished = true, Break = false;
    /*ATTACKS:
     * 1. 1-2-3-4
     * 2. 1-3
     * 3. 2-4
     * 4. 1-4
     * 5. 2-3
     * 6. 4-3-2-1
     * 7. 1-2
     * 8. 3-4
     */
  
    void Start()
    { 
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        MovePosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), gameObject.transform.position.y);
        //Dificulty based on how many hits to destroy (Higher number = easier)
        switch (PlayerPrefs.GetString("level", "Earth"))
        {
            case "Earth":
                DamageDealt = 15;
                break;
            case "Mars":
                DamageDealt = 10;
                break;
            case "Jupiter":
                DamageDealt = 6;
                break;
            case "Saturn":
                DamageDealt = 3;
                break;
            default:
                DamageDealt = Random.Range(4, 30);
                break;

        }

        //Start IEnumerators
        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }
    private IEnumerator Move()
    {
        while (true)
        {
            Debug.Log("Moving Boss");

            while (gameObject.transform.position != MovePosition)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MovePosition, MoveTime * Time.deltaTime);
                yield return null;
            }
            MovePosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), gameObject.transform.position.y);
            StartCoroutine(Shoot());
            yield return new WaitForSeconds(MoveTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerLaser")
        {
            BossHealth -= DamageDealt;
            BossHealthSlider.value = BossHealth;
            if (BossHealth <= 0)
            {
                GameController.Instance.OnBossBattleComplete();
             
               
            }
        }
    }
    private IEnumerator Shoot()
    {
        int SelectedAttack = Random.Range(1, 8);
        Debug.Log("shot attack " + SelectedAttack);
        if (AttackFinished)
        {
            switch (SelectedAttack)
            {
                case 1:
                    if (Break == false)
                    {
                        StartCoroutine(Attack1());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
                case 2:
                    if (Break == false)
                    {
                        StartCoroutine(Attack2());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
                case 3:
                    if (Break == false)
                    {
                        StartCoroutine(Attack3());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
                case 4:
                    if (Break == false)
                    {
                        StartCoroutine(Attack4());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
                case 5:
                    if (Break == false)
                    {
                        StartCoroutine(Attack5());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
                case 6:
                    if (Break == false)
                    {
                        StartCoroutine(Attack6());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
                case 7:
                    if (Break == false)
                    {
                        StartCoroutine(Attack7());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
                case 8:
                    if (Break == false)
                    {
                        StartCoroutine(Attack8());
                        Break = true;
                        AttackFinished = false;
                    }
                    break;
            }
        }
        yield return new WaitForSeconds(CoolDownPeriod);
    }
    private IEnumerator Attack1()
    {
        LaserSfx.Play();
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun1.transform.position, Quaternion.Euler(Vector3.down));
        yield return new WaitForSeconds(TimeBetweenShots);
        LaserSfx.Play();
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun2.transform.position, Quaternion.Euler(Vector3.down));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser3 = (GameObject)Instantiate(Laser, Gun3.transform.position, Quaternion.Euler(Vector3.down));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser4 = (GameObject)Instantiate(Laser, Gun4.transform.position, Quaternion.Euler(Vector3.down));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
        Debug.Log("Attack1 Fired");
    }
    private IEnumerator Attack2()
    {
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun1.transform.position, Quaternion.Euler(Vector3.down));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun3.transform.position, Quaternion.Euler(Vector3.down));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
    }
    private IEnumerator Attack3()
    {
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun2.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun4.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
    }
    private IEnumerator Attack4()
    {
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun1.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun4.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
    }
    private IEnumerator Attack5()
    {
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun2.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun3.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
    }
    private IEnumerator Attack6()
    {
        GameObject FiredLaser3 = (GameObject)Instantiate(Laser, Gun4.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser4 = (GameObject)Instantiate(Laser, Gun3.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun2.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun1.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
    }
    private IEnumerator Attack7()
    {
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun1.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun2.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
    }
    private IEnumerator Attack8()
    {
        LaserSfx.Play();
        GameObject FiredLaser1 = (GameObject)Instantiate(Laser, Gun3.transform.position, Quaternion.Euler(Vector3.forward));
        yield return new WaitForSeconds(TimeBetweenShots);
        GameObject FiredLaser2 = (GameObject)Instantiate(Laser, Gun4.transform.position, Quaternion.Euler(Vector3.forward));
        LaserSfx.Play();
        yield return AttackFinished = true;
        yield return Break = false;
    }
}
