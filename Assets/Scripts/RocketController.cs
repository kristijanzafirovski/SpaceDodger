using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    Vector2 touchpos;
    float deltax,deltay;
    Rigidbody2D rb;
    private AudioSource audioSource;
    [SerializeField] private AudioSource LaserSfx;
    [SerializeField] private GameObject laser, gun,explosion;
    //Boss Battle
    private int health = 100;
    [SerializeField] private Slider healthBar;
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
