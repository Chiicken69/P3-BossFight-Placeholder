using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{

    // private float attackDamage = 10;
    // private float _bulletSpeed = 10;
    // private float attackTime = 5;
    //public GameObject bulletPrefab;
    private GameObject _player;
    private GameObject _firedBullet;
    public float gunDuration =0.08f;

    LineRenderer gunLine;
    
    void Start()
    {
        _player = this.gameObject;
        gunLine = GetComponent<LineRenderer>();
        gunLine.SetWidth(0.2f, 0.2f);
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
            gunLine.SetPosition(0, transform.position);
            if (hit.collider != null)
            {
                gunLine.SetPosition(1, hit.point);
                Debug.Log(hit.collider.name);

            } else
            {
                //gunLine.SetPosition(1, +);
                Debug.DrawRay(transform.position, shot, Color.red);
            }
            StartCoroutine(ShootGun());



        }
    }

    private Vector2 CalculateShot()
    {
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 shot = screenPos - playerPos;
        return shot;
    }

    IEnumerator ShootGun()
    {
        gunLine.enabled = true;
        yield return new WaitForSeconds(gunDuration);
        gunLine.enabled = false;

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
