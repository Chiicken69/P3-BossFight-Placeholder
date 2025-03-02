using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class fireDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    private GameObject _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Trigger Player")
        {
            _player.GetComponent<HealthSystem>().TakeDamage(damage);

        }

    }
}
