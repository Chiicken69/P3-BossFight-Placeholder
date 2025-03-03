using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealhChecker : MonoBehaviour

{

    private HealthSystem _health;
    [SerializeField] private Sprite[] hpSpriteArray;
    //private GameObject hpObject;
    [SerializeField] private Image _hpRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    private void Awake()
    {
        //hpObject = GameObject.Find("HP UI");
        //_hpRenderer = hpObject.GetComponent<SpriteRenderer>();
        HealthSystem _health = this.GetComponent<HealthSystem>();
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
