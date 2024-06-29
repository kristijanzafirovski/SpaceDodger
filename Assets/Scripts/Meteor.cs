using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    float speed = 7;
    
    // Update is called once per frame
  
    public void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit");
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
