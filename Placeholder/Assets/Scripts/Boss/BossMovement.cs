
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private int _phase;
    [SerializeField] private GameObject[] _phaseOne;
    [SerializeField] private GameObject[] _phaseTwo;
    [SerializeField] private GameObject[] _phaseThree;
    [SerializeField] private GameObject[] _phaseFour;

    private Animator _animator;
    private float _phaseLength;
    private float _phaseCount;
    private GameObject _bossImage;
    private int _lastPlayedPhase;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _bossImage = GameObject.Find("BossImage");
    }

    private void Update()
    {

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        switch (_phase)
        {
            case 0:
                _lastPlayedPhase = 0;

                _phaseLength = 3;

                _bossImage.transform.rotation = Quaternion.Euler(0, 0, -180);

                if (!stateInfo.IsName("BossPhaseOne"))
                {
                    _bossImage.transform.rotation = Quaternion.Euler(0, 0, -180);
                    transform.rotation = Quaternion.Euler(0, 0, -180);
                    transform.position = _phaseOne[Random.Range(0, _phaseOne.Length)].transform.position;
                    _animator.Play("BossPhaseOne");
                    _phaseCount++;
                }
                break;

            case 1:
                _lastPlayedPhase = 1;

                _phaseLength = 4;

                transform.rotation = Quaternion.Euler(0, 0, 0);

                if (!stateInfo.IsName("BossPhaseTwoLeft") && !stateInfo.IsName("BossPhaseTwoRight"))
                {
                    if (Random.Range(0, 2) == 0)
                    {

                        _bossImage.transform.rotation = Quaternion.Euler(0, 0, -30);
                        transform.position = _phaseTwo[0].transform.position;
                        _animator.Play("BossPhaseTwoLeft");

                    }
                    else
                    {

                        _bossImage.transform.rotation = Quaternion.Euler(0, 0, 30);
                        transform.position = _phaseTwo[1].transform.position;
                        _animator.Play("BossPhaseTwoRight");

                    }
                    _phaseCount++;
                }
                break;

            case 2:
                _lastPlayedPhase = 2;

                _phaseLength = 3;

                _bossImage.transform.rotation = Quaternion.Euler(0, 0, 0);

                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("BossPhaseOne"))
                {
                    _bossImage.transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.position = _phaseThree[Random.Range(0, _phaseThree.Length)].transform.position;
                    _animator.Play("BossPhaseOne");
                    _phaseCount++;
                }
                break;
        }

        if (_phaseCount >= _phaseLength)
        {
            print("phaseee");
            _phaseCount = 0;

            _phase = Random.Range(0, 3);

            if (_phase == _lastPlayedPhase)
            {
                _phase = Random.Range(0, 3);


                if (_phase == _lastPlayedPhase)
                {
                    _phase = Random.Range(0, 3);

                    if (_phase == _lastPlayedPhase)
                    {
                        _phase = Random.Range(0, 3);
                    }

                }

            }

        }
        
    }

}