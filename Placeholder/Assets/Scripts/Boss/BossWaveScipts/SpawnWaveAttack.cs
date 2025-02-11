using System.Collections;

using UnityEngine;

public class SpawnWaveAttack : MonoBehaviour
{
    class Direction
    {
        public Vector2 North = new Vector2(0, 9);
        public Vector2 East = new Vector2(13, 0);
        public Vector2 South = new Vector2(0, -9);
        public Vector2 West = new Vector2(-13, 0);
    };
    private Vector2 _warningNorth = new Vector2(0, 4.34f);
    private Vector2 _warningSouth = new Vector2(0, -3.68f);
    private Vector2 _warningEast = new Vector2(8.01f, 0);
    private Vector2 _warningWest = new Vector2(-8.01f, 0);



    float _min = -3;
    float _max = 3;
    //float _speed = 0.5f;

    [SerializeField]
    private GameObject _goonPrefab;

    private Goon _goon;

    [SerializeField]
    private GameObject _Warning;

 

    private Direction _enemyDirections; // Global direction instance

    [SerializeField]
    float _HoardDuration = 10f;
    float _WarningDuration = 0.5f;


    Rigidbody goonRigidbody;
    void Start()
    {
        
    }

    //static List<GameObject> _EnemyList = new List<GameObject>();

    void Update()
    {
        _enemyDirections = new Direction();  // Initialize Direction instance
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnWave(6, _enemyDirections.East, 2); // Pass the East vector to spawn enemies from the right
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnWave(6, _enemyDirections.West, 2); // Pass the East vector to spawn enemies from the right
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnWave(6, _enemyDirections.North, 2); // Pass the East vector to spawn enemies from the right
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnWave(6, _enemyDirections.South, 2); // Pass the East vector to spawn enemies from the right
        }
    }

    public void SpawnWave(int Amount, Vector2 Direction, float speed)
    {
        CreateEnemies(Amount, Direction, speed);
    }

    private void CreateEnemies(int Amount, Vector2 Direction, float speed)
    {


        for (int i = 0; i < Amount; i++)
        {
            // Set spawn position based on direction
            Vector3 Spawnpoint = Vector3.zero;

            if (Direction == _enemyDirections.North)
            {

                GameObject WarningClone = Instantiate(_Warning, _warningNorth, Quaternion.identity);


                if (WarningClone != null)
                {
                    Destroy(WarningClone, _WarningDuration);
                }


                Spawnpoint = new Vector3(Random.Range(_min, _max), 9, 0); // Spawn at the top
                Spawnpoint.y += Random.Range(_min, _max);

            }
            else if (Direction == _enemyDirections.South)
            {
                GameObject WarningClone = Instantiate(_Warning, _warningSouth, Quaternion.identity);

                if (WarningClone != null){ 
                Destroy(WarningClone, _WarningDuration);
                }

                Spawnpoint = new Vector3(Random.Range(_min, _max), -9, 0); // Spawn at the bottom
                Spawnpoint.y += Random.Range(_min, _max);
            }
            else if (Direction == _enemyDirections.East)
            {
                GameObject WarningClone = Instantiate(_Warning, _warningEast, Quaternion.identity);
                if (WarningClone != null)
                {
                    Destroy(WarningClone, _WarningDuration);
                }

                Spawnpoint = new Vector3(13, Random.Range(_min, _max), 0); // Spawn on the right
                Spawnpoint.x += Random.Range(_min, _max);
            }
            else if (Direction == _enemyDirections.West)
            {
                GameObject WarningClone = Instantiate(_Warning, _warningWest, Quaternion.identity);
                if (WarningClone != null) { 
                Destroy(WarningClone, _WarningDuration);
                }

                Spawnpoint = new Vector3(-13, Random.Range(_min, _max), 0); // Spawn on the left
                Spawnpoint.x += Random.Range(_min, _max);
            }

     

            GameObject enemy = Instantiate(_goonPrefab, Spawnpoint, Quaternion.identity);
            //_EnemyList.Add(enemy);

  

            StartCoroutine(EnemyMovement(enemy, Direction, speed)); // Pass the specific enemy to the movement coroutine

            if (enemy != null)
            {
                Destroy(enemy, _HoardDuration + 1);
            }
        }
    }

    private IEnumerator EnemyMovement(GameObject enemy, Vector2 Direction, float speed)
    {

        yield return new WaitForSeconds(_WarningDuration);


        float elapsedTime = 0f;
          _goon = enemy.GetComponent<Goon>();

        //goonRigidbody = GetComponent<Rigidbody>();

        float _speedRange = Random.Range(speed-(speed/2), speed+(speed / 2));

        //this belove
        while (elapsedTime < _HoardDuration)
        {
            // Update the position based on the inverted direction
            if (Direction == _enemyDirections.North)
            {

                //Vector2 NorthDir = new Vector2(0,_speed);

                _goon.SetSpeed(_speedRange);
                _goon.SetTarget(Vector2.down);



                /*
                Rigidbody2D enemyRB = enemy.AddComponent<Rigidbody2D>();

                // South
                enemyRB.AddForce(0, Time.deltaTime * -Mathf.Abs(_enemyDirections.North.y) * _speed, 0);

         

                //enemyRB.AddForce(0,Time.deltaTime *-Mathf.Abs(_enemyDirections.North.y*_speed))
                */
            }
            else if (Direction == _enemyDirections.South)
            {
                _goon.SetSpeed(_speedRange);
                _goon.SetTarget(Vector2.up);


                // North
                // enemy.transform.position += new Vector3(0, Time.deltaTime * Mathf.Abs(_enemyDirections.South.y) * _speed, 0);
            }
            else if (Direction == _enemyDirections.East)
            {
                _goon.SetSpeed(_speedRange);
                _goon.SetTarget(Vector2.left);


                //  West
                //enemy.transform.position += new Vector3(Time.deltaTime * -Mathf.Abs(_enemyDirections.East.x)* _speed, 0, 0);
            }
            else if (Direction == _enemyDirections.West)
            {
                _goon.SetSpeed(_speedRange);
                _goon.SetTarget(Vector2.right);

                // East
                //enemy.transform.position += new Vector3(Time.deltaTime * Mathf.Abs(_enemyDirections.West.x) * _speed, 0, 0);
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // After the enemy has finished moving, you can handle any other logic here (like destroying the enemy)
    }
}