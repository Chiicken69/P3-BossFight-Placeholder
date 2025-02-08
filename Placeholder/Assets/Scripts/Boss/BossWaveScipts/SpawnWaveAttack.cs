
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using Random = UnityEngine.Random;

public class SpawnWaveAttack : MonoBehaviour
{
    class Direction
    {
        public Vector2 North = new Vector2(0, 9);
        public Vector2 East = new Vector2(13, 0);
        public Vector2 South = new Vector2(0, -9);
        public Vector2 West = new Vector2(-13, 0);
    };

    [SerializeField]
    private GameObject goonPrefab;
    private Goon goonScript;

    private Direction enemyDirections; // Global direction instance

    void Start()
    {
        enemyDirections = new Direction();  // Initialize Direction instance
        SpawnWave(11, enemyDirections.North); // Pass the North vector
    }

   static List<GameObject> _EnemyList = new List<GameObject>();

    float min = -3;
    float max = 3;
    float x;
    float y;

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWave(int Amount, Vector2 Direction)
    {
        CreateEnemies(Amount, Direction);
    }

    private void CreateEnemies(int Amount, Vector2 Direction)
    { 
        for (int i = 0; i <= Amount; i++)
        {   
            
            if (Direction == enemyDirections.North)
            {
                     x = Random.Range(min, max);

                     y = Random.Range(enemyDirections.North.y, max + enemyDirections.North.y);

                   //break;
            }
            if (Direction == enemyDirections.South)
            {

                 x = Random.Range(min, max);

                 y = Random.Range(enemyDirections.South.y, max - enemyDirections.South.y);
            }
            if (Direction == enemyDirections.East)
            {
                 x = Random.Range(enemyDirections.East.x, max + enemyDirections.East.x);

                 y = Random.Range(min, max);
            }
            if (Direction == enemyDirections.West)
            {
                 x = Random.Range(enemyDirections.West.x, max - enemyDirections.West.x);
                 y = Random.Range(min, max);

            }
            Vector3 Spawnpoint = new Vector3(x, y, 0);


            GameObject enemies = Instantiate(goonPrefab, Spawnpoint, quaternion.identity);
            _EnemyList.Add(enemies);

            //goonScript = GameObject.FindFirstObjectByType<Goon>();
            //_EnemyList[i].goonScript.InitializeEnemy(Direction);
        }
    }
}
