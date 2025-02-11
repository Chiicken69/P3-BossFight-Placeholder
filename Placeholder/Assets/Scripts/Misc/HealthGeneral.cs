using UnityEngine;

public class HealthGeneral : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;


    private void Awake()
    {
        InitializeHealth(_maxHealth);
      
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int DamageToTake)
    {
       
        _currentHealth -= DamageToTake;
        
       
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







}
