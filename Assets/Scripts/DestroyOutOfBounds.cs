using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float yBound = 15.5f;

    private float xBound = 24.0f;

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x < -xBound){
            Destroy(gameObject);
        }
        if(transform.position.x > xBound){
            Destroy(gameObject);
        }

        if(transform.position.y < -yBound){
            Destroy(gameObject);
        }
        if(transform.position.y > yBound) {
            Destroy(gameObject);
        }

    }
}
