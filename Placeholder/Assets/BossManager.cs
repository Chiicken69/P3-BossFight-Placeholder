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

    Vector2[] spawnPositions = new Vector2[]
    {
            new Vector2(0, 9),
            new Vector2(13, 0),
            new Vector2(0, -9),
            new Vector2(-13, 0)
    };

    private float _hordeTimer = 0f;

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
            transform.position = new Vector3(8, -4, 0);
            GetComponent<PortalFist>().enabled = false;
            GetComponent<BossFireAttack>().enabled = false;
            GetComponent<BossMovement>().enabled = false;
            GetComponent<SpawnWaveAttack>().enabled = true;

            _hordeTimer += Time.deltaTime;

            if (_hordeTimer > 3)
            {


                _hordeTimer = 0;
                Vector2[] positions = { new Vector2(0, 9), new Vector2(13, 0), new Vector2(0, -9), new Vector2(-13, 0) };
                GetComponent<SpawnWaveAttack>().SpawnWave(Random.Range(6, 13), positions[Random.Range(0, positions.Length)], 3);
            }

        }
        else
        {
            gameObject.SetActive(false);
            print("YOU WIN!!!");
        }


    }
}
