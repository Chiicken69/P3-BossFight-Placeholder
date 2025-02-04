using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{

    // private float attackDamage = 10;
    private float _bulletSpeed = 10;
    // private float attackTime = 5;
    public GameObject bulletPrefab;
    private GameObject _player;
    private GameObject _firedBullet;
    public float gameZ = 8;
    
    void Start()
    {
        _player = this.gameObject;
    }
    void Update()
    {
      FireShot();  
    }

    private void FireShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
          Vector2 shot = CalculateShot();
          RaycastHit2D hit = Physics2D.Raycast(transform.position, shot, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
            Debug.DrawRay(transform.position, shot, Color.red);
        }
    }

    private Vector2 CalculateShot()
    {
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 shot = screenPos - playerPos;
        return shot;
    }
    
    
    

    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cloneBullet = Instantiate(bullet, player.transform);
            directionToShoot = cloneBullet.transform.forward - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cloneBullet.transform.Translate(directionToShoot * bulletSpeed * Time.deltaTime);
        }


    }


        Vector2 screen_mouse_pos = Input.mousePosition;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(screen_mouse_pos.x, screen_mouse_pos.y, game_z));

        if (Input.GetMouseButtonDown(0))
        {
            GameObject fired_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Vector3 bullet_direction = new Vector3(mousePos.x, mousePos.y, game_z) - transform.position;
            fired_bullet.transform.Translate(bullet_direction * bulletSpeed * Time.deltaTime);

            Destroy(fired_bullet,1f);
        }
    */

}
