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

    public Component PortalFist;

    //fireball



    //horde

    private void Start()
    {
        maxBossHealth = GetComponent<HealthGeneral>()._maxHealth;
        PortalFist = GetComponent<PortalFist>();
    }

    void Update()
    {

        bossHealth = GetComponent<HealthGeneral>()._currentHealth;
        print(bossHealth);

        if (bossHealth >= 600)
        {
            print("phase1");

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
        else if (bossHealth >= 300)
        {
            GetComponent<PortalFist>().enabled = false;
            GetComponent<BossFireAttack>().enabled = true;
            GetComponent<BossMovement>().enabled = true;
            GetComponent<SpawnWaveAttack>().enabled = false;
            print("phase2");

        }
        else if (bossHealth >= 0)
        {
            print("phase3");

            GetComponent<PortalFist>().enabled = false;
            GetComponent<BossFireAttack>().enabled = false;
            GetComponent<BossMovement>().enabled = false;
            GetComponent<SpawnWaveAttack>().enabled = false;



        }
        else
        {
            gameObject.SetActive(false);
            print("YOU WIN!!!");
        }


    }
}
