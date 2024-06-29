using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    float speed = 7;

    // Update is called once per frame

    public void OnCollisionEnter2D(Collision2D coll)
    {
       
        if (coll.collider.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {

        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
