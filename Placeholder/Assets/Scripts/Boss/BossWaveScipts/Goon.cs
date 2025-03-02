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

    [SerializeField] private float goonerTimer;

    [SerializeField] private int goonDamage;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    bool inview = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(7, 8);
        Physics2D.IgnoreLayerCollision(7, 9);
        Physics2D.IgnoreLayerCollision(7, 6);
        Physics2D.IgnoreLayerCollision(7, 3);

        _goonRB = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player");
    }





    
    float _speed;
    Vector2 _target;


    private void FixedUpdate()
    {
        goonerTimer -= Time.deltaTime; 

        print(_speed);
        _goonRB.AddForce(_target * _speed);

        if (goonerTimer <= 0)
        {
            Destroy(this.gameObject);
        }


        if (spriteRenderer.isVisible == true && inview == false)
        {
            inview= true;
            AudioManager.Instance.PlaySFXArrayRandom("GoonSpawnSound");
            Debug.Log("Object is in view!");
        }
        else if(spriteRenderer.isVisible != true)
        {
            inview = false; //ik fucked i do this each frame
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
   


            if (collision.gameObject.name == "Trigger Player")
            {
                player.GetComponent<HealthSystem>().TakeDamage(goonDamage);

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
