using UnityEngine;

public class BossMovement : MonoBehaviour
{

    [SerializeField] private int _phase;

    [SerializeField] private GameObject[] _phaseOne;


    // Update is called once per frame
    void Update()
    {
        switch (_phase)
        {

            case 0:

                transform.position = _phaseOne[Random.Range(0, _phaseOne.Length)].transform.position;



                break;

            default:
                break;
        }

    }
}
