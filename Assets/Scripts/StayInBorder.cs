﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInBorder : MonoBehaviour
{

    void Update()
    {
        if (Spawner.Instance.StartSpawning)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2f, 2f),
            Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);
        }
    }
}
