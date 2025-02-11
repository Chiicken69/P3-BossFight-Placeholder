using System;
using UnityEngine;

public class Goon : MonoBehaviour
{

    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            //Output the Collider's GameObject's name
            Debug.Log(collision.collider.name);


        }
    }

}
