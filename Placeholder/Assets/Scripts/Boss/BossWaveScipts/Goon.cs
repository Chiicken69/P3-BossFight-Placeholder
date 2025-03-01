using System;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Goon : MonoBehaviour
{

    Rigidbody2D _goonRB;

    [SerializeField] public float goonerTimer;
    [SerializeField] public int goonDamage;
    private GameObject player;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(7,8);
        Physics2D.IgnoreLayerCollision(7, 2);
        Physics2D.IgnoreLayerCollision(7,9);
        _goonRB = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player");
    }

    /*
    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.name == "Player") // Threshold for "collision"
        {
            //Output the Collider's GameObject's name

            player.GetComponent<HealthSystem>().TakeDamage(goonDamage);
            
        }
    }
    */
    float _speed;
    Vector2 _target;

    [SerializeField]
    float _timer = 0f;
    [SerializeField]
    float _resetTimer = 1f;

    private void FixedUpdate()
    {
        _timer-= Time.deltaTime; 

        print(_speed);
        _goonRB.AddForce(_target * _speed);

        if (goonerTimer == 0)
        {
            Destroy(this.gameObject);
        }


        if (_timer <= 0) // Only deal damage when the timer has expired
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 1.0f) // Threshold for "collision"
            {
                player.GetComponent<HealthSystem>().TakeDamage(goonDamage);
                _timer = _resetTimer; // Reset the timer so damage isn't applied immediately next frame
            }
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
