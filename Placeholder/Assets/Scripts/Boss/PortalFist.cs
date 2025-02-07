using UnityEngine;

public class PortalFist : MonoBehaviour
{
    [SerializeField] private GameObject _levelAreaBox;
    [SerializeField] private GameObject _portalFistPrefab;
    private GameObject _player;
    private Transform _playerTrans;
    private Transform _levelArea;

    private void Awake()
    {
        // Gets transform for play area
        _levelArea = _levelAreaBox.GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTrans = _player.transform;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") == 1)
        {
            SpawnPortalFist();
        }
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
