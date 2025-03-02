using UnityEngine;
public class HealthGeneral : MonoBehaviour
{
    [SerializeField] public int _currentHealth { get; set; }
    [SerializeField] public int _maxHealth { get; set; }

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
        CheckForDeath(_currentHealth);
        


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
            if (!gameObject.CompareTag("Boss"))
            {
                Destroy(this.gameObject);    
            }
            else
            {
                print("The boss cant die >:)");
            }
            
        }
    }
}