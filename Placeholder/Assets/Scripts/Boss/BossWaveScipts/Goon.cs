using System;
using System.Threading;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Goon : MonoBehaviour
{

    Rigidbody2D _goonRB;

    [SerializeField] public float goonerTimer;


    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(7,8);
        Physics2D.IgnoreLayerCollision(7,2);
        Physics2D.IgnoreLayerCollision(7,9);
        _goonRB = GetComponent<Rigidbody2D>();

  
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            //Output the Collider's GameObject's name
            //Debug.Log(collision.collider.name);


        }
    }

    float _speed;
    Vector2 _target;

    private void FixedUpdate()
    {
        goonerTimer -= Time.deltaTime;
        //print(_speed);
        _goonRB.AddForce(_target * _speed);

        if (goonerTimer <= 0)
        { 
        Destroy(this.gameObject);
        }
    }

    public void SetTarget(Vector2 target)
    {
        _target = target;



      
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
