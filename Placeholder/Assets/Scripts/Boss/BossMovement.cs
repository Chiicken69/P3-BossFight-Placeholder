using UnityEngine;

public class BossMovement : MonoBehaviour
{

    [SerializeField] private int _phase;

    [SerializeField] private GameObject[] _phaseOne;

    [SerializeField] private GameObject[] _phaseTwo;

    private Animator _animator;

    private float _phaseLength;

    private float _phaseCount;

    private GameObject _BossImage;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _BossImage = GameObject.Find("BossImage");

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

                if (_animator.GetBool("PhaseTwoRight") == false && _animator.GetBool("PhaseTwoLeft") == false)
                {
                    if (Random.Range(0, 2) == 0)
                    {

                        _animator.SetBool("PhaseTwoRight", false);

                        _BossImage.transform.rotation = Quaternion.Euler(0, 0, -30);

                        transform.position = _phaseTwo[0].transform.position;

                        _animator.SetBool("PhaseTwoLeft", true);

                        _phaseCount++;


                    }
                    else
                    {
                        _animator.SetBool("PhaseTwoLeft", false);

                        _BossImage.transform.rotation = Quaternion.Euler(0, 0, 30);

                        transform.position = _phaseTwo[1].transform.position;

                        _animator.SetBool("PhaseTwoRight", true);

                        _phaseCount++;

                    }
                }
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        

        if (_animator.GetBool("BossIdle") == true)
        {
            _animator.SetBool("PhaseTwoLeft", false);
            _animator.SetBool("PhaseTwoRight", false);
        }

        /*

        if (_phaseCount == _phaseLength)
        {
            _phaseCount = 0;

            print("phase complete");

            _BossImage.transform.rotation = Quaternion.Euler(0, 0, 0);

            _animator.SetBool("PhaseOne", false);
            _animator.SetBool("PhaseTwoRight", false);
            _animator.SetBool("PhaseTwoLeft", false);

        }
        */
    }
}
