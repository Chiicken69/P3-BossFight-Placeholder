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
    CheckForDeath(_currentHealth);
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

    public void CheckForDeath(int hp)
    {
        if (hp <= 0)
        {
<<<<<<< Updated upstream
            Destroy(this.gameObject);
=======
            if (!gameObject.CompareTag("Boss"))
            {
                AudioManager.Instance.PlaySFX("GoonDeath");
                Destroy(this.gameObject);    
            }
            else
            {
                print("The boss cant die >:)");
            }
            
>>>>>>> Stashed changes
        }
    }
}