using UnityEngine;
public class PortalFist : MonoBehaviour
{
    [SerializeField] private GameObject _levelAreaBox;
    [SerializeField] private GameObject _portalFistPrefab;
    [SerializeField] private float _timeToActivatehitbox;
    [SerializeField] private float _portalSpawnDistanceDiff;
    private GameObject _player;
    private PlayerMovement _playerMovement;
    private Transform _playerTrans;
    private Transform _levelArea;
    private Animator _portalAnimator;

    private Vector2 TempTarget;
    private float timer1 = 1;
    private float timerReset = 1;
    private void Awake()
    {
        // Gets transform for play area
        _levelArea = _levelAreaBox.GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTrans = _player.transform;
        _playerMovement = _player.GetComponent<PlayerMovement>();
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
        AudioManager.Instance.PlaySFXArrayRandom("PortalSound");
        Vector3 _target = CalculatePortalPos();
        Debug.Log("Target Vector: " + _target);
        _target = new Vector3(_target.x, (_target.y) + 1, 0);
        GameObject _tempPortalFist = Instantiate(_portalFistPrefab, _target, Quaternion.identity);
        // _tempPortalFist.GetComponent<PortalFistPrefab>().SetHitboxActive(0.9f);
        // _portalAnimator = _tempPortalFist.GetComponent<Animator>();

    }
    private Vector3 CalculatePortalPos()
    {
        Vector2 PlayerMoveDir = _playerMovement.readMoveDir;
        PlayerMoveDir.Normalize();
        print("Player movement direction: " + PlayerMoveDir);
        TempTarget = new Vector2(0, 0);
        if (_playerTrans.position.x > 0)
        {
            TempTarget.x += _playerTrans.position.x;

        }
        else
        {
            TempTarget.x += _playerTrans.position.x;

        }
        if (_playerTrans.position.y > 0)
        {
            TempTarget.y += _playerTrans.position.y;
        }
        else
        {
            TempTarget.y += _playerTrans.position.y;
        }

        if (PlayerMoveDir.x > 0)
        {
            TempTarget.x += _portalSpawnDistanceDiff;
        }
        else if (PlayerMoveDir.x < 0)
        {
            TempTarget.x -= _portalSpawnDistanceDiff;
        }
        if (PlayerMoveDir.y > 0)
        {
            TempTarget.y += _portalSpawnDistanceDiff;
        }
        else if (PlayerMoveDir.y < 0)
        {
            TempTarget.y -= _portalSpawnDistanceDiff;
        }

        return TempTarget;
    }
}