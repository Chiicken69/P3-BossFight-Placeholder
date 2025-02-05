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

    float _gunDuration =0.08f;

    private int _remainingBullets;

    LineRenderer gunLine;

    [SerializeField]
    float _timer = 0.5f;

    [SerializeField]
    float _resetTimer = 0.5f;

    private int currentAmmoLoaded;
    private int maxAmmoCapacity = 6;

    void Start()
    {
        _player = this.gameObject;
        gunLine = GetComponent<LineRenderer>();
        gunLine.SetWidth(0.2f, 0.2f);
    }
    void Update()
    {
      FireShot();
        Reload();
      _timer -= Time.deltaTime;
    }

    //this funktion uses calculate shot to shoot out a raycast where mouse is clicked and then uses another raycast to check if it actually hit any collider
    //it wont shoot out a ray if there is no collider to hit.
    private void FireShot()
    {
        Debug.Log(currentAmmoLoaded);
        if (Input.GetMouseButtonDown(0) && _timer < 0 && currentAmmoLoaded > 0)
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
            --currentAmmoLoaded;
            _timer = _resetTimer;



        }
    }
 


    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int reloadAmount = maxAmmoCapacity - currentAmmoLoaded;
            reloadAmount = (currentAmmoLoaded + reloadAmount) >= 0 ? reloadAmount : currentAmmoLoaded;
            currentAmmoLoaded += reloadAmount;
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
        yield return new WaitForSeconds(_gunDuration);
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
