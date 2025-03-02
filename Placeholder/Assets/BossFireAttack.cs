using UnityEngine;

public class BossFireAttack : MonoBehaviour
{

    public bool theBossShouldAttack;

    [SerializeField] private GameObject player;

    private float _attackCooldown;

    [SerializeField] private GameObject fireBall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _attackCooldown -= Time.deltaTime;

        if (theBossShouldAttack && _attackCooldown < 0)
        {
            theBossShouldAttack = false;

            print("atack");

            _attackCooldown = 0.1f;

            GameObject fireball = Instantiate(fireBall, transform.position, Quaternion.identity);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

            // Calculate direction toward player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Rotate fireball to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fireball.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Apply force to move the fireball
            rb.linearVelocity = direction * 10;

        }

    }
}
