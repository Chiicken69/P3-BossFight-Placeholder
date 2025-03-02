
using System.Collections;

using UnityEngine;


public class PlayerAttack : MonoBehaviour
{

    // private float attackDamage = 10;
    // private float _bulletSpeed = 10;
    // private float attackTime = 5;
    //public GameObject bulletPrefab;
    private GameObject _player;
    private GameObject _firedBullet;
    private GameObject _uiAmmo;
    [SerializeField]
    private GameObject _reloadingText;

    [SerializeField]
    private int revolverDamage;

    [SerializeField]
    float _gunDuration =0.03f;

    LineRenderer gunLine;

    [SerializeField]
    float _timer = 0.25f;

    [SerializeField]
    float _resetTimer = 0.25f;

    [SerializeField]
    float _reloadTimer = 2f;


    [SerializeField] private GameObject _partikalObject;
    [SerializeField] private GameObject _hitWallPartikalObject;

    private int _currentAmmoLoaded;
    private int _maxAmmoCapacity = 6;

    private Animator animator;

    bool _Reloading = false;

    [SerializeField] private LayerMask layerMask;

    public UnityEngine.Color lineColor = UnityEngine.Color.gray;
    void Start()
    {

        _uiAmmo = GameObject.Find("UI Ammo");
        animator = _uiAmmo.GetComponent<Animator>();
        _player = this.gameObject;
        gunLine = GetComponent<LineRenderer>();
        gunLine.SetWidth(0.2f, 0.2f);
        Physics2D.IgnoreLayerCollision(2, 10);
        Physics2D.IgnoreLayerCollision(3, 2);
        Physics2D.IgnoreLayerCollision(9, 3);
    }
    void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 3);
        FireShot();
        Reload();
        AmmoUI(_currentAmmoLoaded);
      _timer -= Time.deltaTime;
    }

    //this funktion uses calculate shot to shoot out a raycast where mouse is clicked and then uses another raycast to check if it actually hit any collider
    //it wont shoot out a ray if there is no collider to hit.
    private void FireShot()
    {
        //when you run out of bullets it reloads automatically
        if (_currentAmmoLoaded == 0 && _Reloading == false)
        {
            StartCoroutine(ReloadTimer()); 
        }
        // if left click go boom
        if (Input.GetMouseButtonDown(0) && _timer < 0 && _currentAmmoLoaded > 0 && _Reloading == false)
        {
            //call sfx
            AudioManager.Instance.PlaySFX("gunShot");
            //checks if raycast hits collider then do damage and play partikal
            Vector2 shot = CalculateShot();
          RaycastHit2D hit = Physics2D.Raycast(transform.position, shot, Mathf.Infinity, layerMask);
            Debug.Log("LayerMask Value: " + layerMask.value);
            gunLine.SetPosition(0, transform.position);
            gunLine.startColor = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 0.7f);;
            if (hit.collider != null)
            {
                var main = _hitWallPartikalObject.GetComponent<ParticleSystem>().main;
                main.startColor = new UnityEngine.Color(255, 255, 255, 255);

                if (hit.collider.name == "Boss" || hit.collider.name == "Goon(Clone)")
                {
                    
                    main = _hitWallPartikalObject.GetComponent<ParticleSystem>().main;
                    main.startColor = new UnityEngine.Color(111111, 0, 0, 0);

                    
                    hit.collider.GetComponent<HealthGeneral>().TakeDamage(revolverDamage);
                    
                }

                gunLine.SetPosition(1, hit.point);
                Debug.Log(hit.collider.name);
                GameObject tempHitParticlObject = Instantiate(_hitWallPartikalObject, hit.point, Quaternion.identity); //make hit effet red?

                Destroy(tempHitParticlObject, 1);

            }
            // Calculates the Quaternion rotation of Gun spark yellow partical effect 
            float angle = Mathf.Atan2(shot.y, shot.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject tempParticlObject = Instantiate(_partikalObject, transform.position, rotation);

            Destroy(tempParticlObject, 1);

            //plays the shoot gun coroutine and lowers loaded ammo
            StartCoroutine(ShootGun());
            --_currentAmmoLoaded;
            _timer = _resetTimer;
            Debug.Log(_currentAmmoLoaded);


        }
    }


    // if press R then reload and run coroutine timer 
    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && _Reloading == false)
        {
            
            StartCoroutine(ReloadTimer());
        }
    }
    IEnumerator ReloadTimer()
    {
        AudioManager.Instance.PlaySFX("gunReload");
        _reloadingText.SetActive(true);
        _Reloading = true;
        yield return new WaitForSeconds(_reloadTimer);
        int _reloadAmount = _maxAmmoCapacity - _currentAmmoLoaded; //check how much need to reload

        // if reload geather than or equal to 0 bullets then return reload amount otherwise return currentammo
        _reloadAmount = (_currentAmmoLoaded + _reloadAmount) >= 0 ? _reloadAmount : _currentAmmoLoaded;
        _currentAmmoLoaded += _reloadAmount;
        _Reloading = false;
        _reloadingText.SetActive(false);
    }

    private void AmmoUI(int _currentAmmo)
    {
        animator.SetInteger("AmmoCount", _currentAmmo); //updated the ammo ui
    }

    //calculates where i shot in the world
    private Vector2 CalculateShot()
    {
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 shot = screenPos - playerPos;
        return shot;
    }

    //draws the gunline trail for however long _gunDuration is 
    IEnumerator ShootGun()
    {
        gunLine.enabled = true;
     
        yield return new WaitForSeconds(_gunDuration);
        gunLine.enabled = false;
    }

}
