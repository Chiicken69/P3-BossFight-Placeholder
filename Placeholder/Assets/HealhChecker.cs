using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealhChecker : MonoBehaviour

{    private GameObject _player;
    private HealthSystem _health;
    [SerializeField] private Sprite[] hpSpriteArray;
    private GameObject hpObject;
    private SpriteRenderer _hpRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpObject = GameObject.Find("HP UI");
        _player = GameObject.FindGameObjectWithTag("Player");
        _hpRenderer = hpObject.GetComponent<SpriteRenderer>();
        HealthSystem _health = _player.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health._currentHealth == 30)
        {
            _hpRenderer.sprite = hpSpriteArray[0];
        }
        if (_health._currentHealth == 20)
        {
            _hpRenderer.sprite = hpSpriteArray[1];
        }
        if (_health._currentHealth == 10)
        {
            _hpRenderer.sprite = hpSpriteArray[2];
        }
    }
}
