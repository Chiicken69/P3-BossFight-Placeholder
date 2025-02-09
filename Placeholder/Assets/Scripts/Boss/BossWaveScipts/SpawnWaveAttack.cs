using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Vector2 warningNorth = new Vector2(0, 4.34f);
    public Vector2 warningSouth = new Vector2(0, -4.34f);
    public Vector2 warningEast = new Vector2(8.01f, 0);
    public Vector2 warningWest = new Vector2(-8.01f, 0);



    float min = -3;
    float max = 3;
    float speed = 0.5f;

    [SerializeField]
    private GameObject goonPrefab;

    [SerializeField]
    private GameObject Warning;

    private Direction enemyDirections; // Global direction instance

    [SerializeField]
    float _HoardDuration = 10f;
    float _WarningDuration = 0.5f;

    void Start()
    {
        enemyDirections = new Direction();  // Initialize Direction instance
        SpawnWave(6, enemyDirections.East); // Pass the East vector to spawn enemies from the right
    }

    static List<GameObject> _EnemyList = new List<GameObject>();

    void Update()
    {

    }

    public void SpawnWave(int Amount, Vector2 Direction)
    {
        CreateEnemies(Amount, Direction);
    }

    private void CreateEnemies(int Amount, Vector2 Direction)
    {


        for (int i = 0; i < Amount; i++)
        {
            // Set spawn position based on direction
            Vector3 Spawnpoint = Vector3.zero;

            if (Direction == enemyDirections.North)
            {

                GameObject WarningClone = Instantiate(Warning, warningNorth, Quaternion.identity);
                Destroy(WarningClone, _WarningDuration);

                Spawnpoint = new Vector3(Random.Range(min, max), 9, 0); // Spawn at the top
                Spawnpoint.y += Random.Range(min, max);

            }
            else if (Direction == enemyDirections.South)
            {
                GameObject WarningClone = Instantiate(Warning, warningSouth, Quaternion.identity);
                Destroy(WarningClone, _WarningDuration);

                Spawnpoint = new Vector3(Random.Range(min, max), -9, 0); // Spawn at the bottom
                Spawnpoint.y += Random.Range(min, max);
            }
            else if (Direction == enemyDirections.East)
            {
                GameObject WarningClone = Instantiate(Warning, warningEast, Quaternion.identity);
                Destroy(WarningClone, _WarningDuration);


                Spawnpoint = new Vector3(13, Random.Range(min, max), 0); // Spawn on the right
                Spawnpoint.x += Random.Range(min, max);
            }
            else if (Direction == enemyDirections.West)
            {
                GameObject WarningClone = Instantiate(Warning, warningWest, Quaternion.identity);
                Destroy(WarningClone, _WarningDuration);

                Spawnpoint = new Vector3(-13, Random.Range(min, max), 0); // Spawn on the left
                Spawnpoint.x += Random.Range(min, max);
            }

            GameObject enemy = Instantiate(goonPrefab, Spawnpoint, Quaternion.identity);
            _EnemyList.Add(enemy);

            StartCoroutine(EnemyMovement(enemy, Direction)); // Pass the specific enemy to the movement coroutine
            Destroy(enemy, _HoardDuration);
        }
    }

    private IEnumerator EnemyMovement(GameObject enemy, Vector2 Direction)
    {

        yield return new WaitForSeconds(_WarningDuration);


        float elapsedTime = 0f;



        //this belove
        while (elapsedTime < _HoardDuration)
        {
            // Update the position based on the inverted direction
            if (Direction == enemyDirections.North)
            {

     
                
                // South
                enemy.transform.position += new Vector3(0, Time.deltaTime * -Mathf.Abs(enemyDirections.North.y) * speed, 0);

            }
            else if (Direction == enemyDirections.South)
            {
            
        

                // North
                enemy.transform.position += new Vector3(0, Time.deltaTime * Mathf.Abs(enemyDirections.South.y) * speed, 0);
            }
            else if (Direction == enemyDirections.East)
            {
               
                //  West
                enemy.transform.position += new Vector3(Time.deltaTime * -Mathf.Abs(enemyDirections.East.x)* speed, 0, 0);
            }
            else if (Direction == enemyDirections.West)
            {
             
    
                // East
                enemy.transform.position += new Vector3(Time.deltaTime * Mathf.Abs(enemyDirections.West.x) * speed, 0, 0);
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // After the enemy has finished moving, you can handle any other logic here (like destroying the enemy)
    }
}