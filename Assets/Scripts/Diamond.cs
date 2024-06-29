using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    float speed = 7;

    // Update is called once per frame
    private void Awake()
    {

    }
    public void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Collected diamond");
        if (coll.collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {

        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
