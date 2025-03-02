using System;
using System.Runtime.Serialization;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    
   [SerializeField] public int _currentHealth;
   [SerializeField] private int _maxHealth;
   
   private PlayerMovement _playerMovement;
    


    private void Awake()
    {
        InitializeHealth(_maxHealth);
        _playerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        
    }


    public void TakeDamage(int DamageToTake)
    {
        if (!_playerMovement.isInvincible)
        {
            _currentHealth -= DamageToTake;    
        }
        else
        {
            Debug.Log("Player is invincible");
        }
        
    }

    public void GainHealth(int HealthToGain)
    {
      
        int diff = HealthToGain + _currentHealth;


        if (diff < _maxHealth) 
        {
            _currentHealth += HealthToGain;
        }
        
    }

    public void InitializeHealth(int hp)
    {
       _currentHealth = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PortalFist"))
        {
            TakeDamage(1);
            Debug.Log("Player took" + 1 + " Damage, from portal fist");
        }
    }
}
