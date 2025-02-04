using System;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    [SerializeField] private int _damage;

    private BoxCollider2D _collider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(_damage);
            Debug.Log("Player took " + _damage);
        }
       
    }
}
