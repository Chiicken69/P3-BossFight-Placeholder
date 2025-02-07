using UnityEngine;

public class PortalFist : MonoBehaviour
{
    [SerializeField] private GameObject _levelAreaBox;
    [SerializeField] private GameObject _portalFistPrefab;
    private GameObject _player;
    private Transform _playerTrans;
    private Transform _levelArea;

    private float timer1 = 1;
    private float timerReset = 1;
    private void Awake()
    {
        // Gets transform for play area
        _levelArea = _levelAreaBox.GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTrans = _player.transform;
        timer1 = timerReset;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") == 1 && timer1 <= 0)
        {
            SpawnPortalFist();
            timer1 = timerReset;
        }
    }
    private void FixedUpdate()
    {
        timer1 -= Time.deltaTime;
    }


    public void SpawnPortalFist()
    {
        Vector3 _target = CalculatePortalPos();
        Debug.Log("Target Vector: " + _target);

        GameObject _tempPortalFist = Instantiate(_portalFistPrefab, _target, Quaternion.identity);


    }

    private Vector3 CalculatePortalPos()
    {
        
        return _playerTrans.position;

    }
}
