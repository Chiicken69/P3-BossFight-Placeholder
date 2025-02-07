
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private int _phase;
    [SerializeField] private GameObject[] _phaseOne;
    [SerializeField] private GameObject[] _phaseTwo;

    private Animator _animator;
    private float _phaseLength;
    private float _phaseCount;
    private GameObject _bossImage;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _bossImage = GameObject.Find("BossImage");
    }

    private void FixedUpdate()
    {
        switch (_phase)
        {
            case 0:
                _phaseLength = 3;

                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("BossPhaseOne"))
                {
                    transform.rotation = Quaternion.Euler(0, 0, -180);
                    transform.position = _phaseOne[Random.Range(0, _phaseOne.Length)].transform.position;
                    _animator.SetBool("PhaseOne", true);
                    _phaseCount++;
                }
                break;

            case 1:
                _phaseLength = 4;

                if (!_animator.GetBool("PhaseTwoRight") && !_animator.GetBool("PhaseTwoLeft"))
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        _animator.SetBool("PhaseTwoRight", false);
                        _bossImage.transform.rotation = Quaternion.Euler(0, 0, -30);
                        transform.position = _phaseTwo[0].transform.position;
                        _animator.SetBool("PhaseTwoLeft", true);
                    }
                    else
                    {
                        _animator.SetBool("PhaseTwoLeft", false);
                        _bossImage.transform.rotation = Quaternion.Euler(0, 0, 30);
                        transform.position = _phaseTwo[1].transform.position;
                        _animator.SetBool("PhaseTwoRight", true);
                    }
                    _phaseCount++;
                }
                break;
        }
    }

    private void Update()
    {
        if (_animator.GetBool("BossIdle"))
        {
            _animator.SetBool("PhaseTwoLeft", false);
            _animator.SetBool("PhaseTwoRight", false);
        }

        /* Uncomment this section if you want to reset phase after phaseCount reaches phaseLength
        if (_phaseCount >= _phaseLength)
        {
            _phaseCount = 0;
            Debug.Log("Phase complete");
            _bossImage.transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("PhaseOne", false);
            _animator.SetBool("PhaseTwoRight", false);
            _animator.SetBool("PhaseTwoLeft", false);
        }
        */
    }
}