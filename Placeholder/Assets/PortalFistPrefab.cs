using Unity.VisualScripting;
using UnityEngine;
public class PortalFistPrefab : MonoBehaviour
{
    private float _timer = 1;
    private float _timerHitbox = 0.5f;
    private BoxCollider2D _hitbox;
    private GameObject _childObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void Awake()
    {
        _hitbox = GetComponentInChildren<BoxCollider2D>(true);
        _childObject = _hitbox.gameObject;



    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        _timer -= Time.deltaTime;
        _timerHitbox -= Time.deltaTime;
        if (_timer < 0)
        {
            Destroy(this.gameObject);
        }
        if (_timerHitbox < 0)
        {
            _childObject.SetActive(true);
        }
    }
    public void SetHitboxActive(float time)
    {
        _timerHitbox = time;
    }
}