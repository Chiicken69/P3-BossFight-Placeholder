using UnityEngine;

public class BossManager : MonoBehaviour
{
    private float _timer = 0f;
    private int _stateIndex = 0;

    //health

    int bossHealth;

    int maxBossHealth;

    //portal fist

    private float _portalFistTimer = 0f;

    //fireball



    //horde

    private void Start()
    {
        maxBossHealth = GetComponent<HealthGeneral>()._maxHealth;
    }

    void Update()
    {

        bossHealth = GetComponent<HealthGeneral>()._currentHealth;

        if (bossHealth > (maxBossHealth) * (2/3))
        {


            GetComponent<BossFireAttack>().enabled = false;
            GetComponent<BossMovement>().enabled = false;
            GetComponent<SpawnWaveAttack>().enabled = false;

            transform.position = new Vector3(0, 5, 0);

            _portalFistTimer += Time.deltaTime;

            if (_portalFistTimer > 2)
            {
                GetComponent<PortalFist>().SpawnPortalFist();

                _portalFistTimer = 0;

            }
        }
        else if (bossHealth > (maxBossHealth) * (1 / 3))
        {

        }
        else if (bossHealth > 0)
        {

        }
        else
        {
            print("YOU WIN!!!");
        }


    }
}
