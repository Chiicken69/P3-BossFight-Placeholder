
using System;


using UnityEngine;

using Random = UnityEngine.Random;

public class SpawnWaveAttack : MonoBehaviour
{
    class Direction { public Vector2 North; public Vector2 East; public Vector2 South; public Vector2 West; };


    void Start()
    {
     


    }
 
  

    float min = 3;
    float max = 3;

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWave(int Amount, Vector2 Direction, Enum EnemyVariant)
    {
        CreateEnemies(Amount, Direction, EnemyVariant);
    }

    private void CreateEnemies(int Amount, Vector2 Direction, Enum EnemyVariant)
    {
        Direction EnemyDir = new Direction();
        EnemyDir.North = new Vector2(0, 9);
        EnemyDir.South = new Vector2(0, -9);
        EnemyDir.East = new Vector2(13, 0);
        EnemyDir.West = new Vector2(-13, 0);

        for (int i = 0; i <= Amount; i++)
        {   
            
            if (Direction == EnemyDir.North)
            {
                    float x = Random.Range(min, max);

                    float y = Random.Range(EnemyDir.North.y, max + EnemyDir.North.y);

                   //break;
            }
            if (Direction == EnemyDir.South)
            {

                float x = Random.Range(min, max);

                float y = Random.Range(EnemyDir.South.y, max - EnemyDir.South.y);
            }
            if (Direction == EnemyDir.East)
            {
                float x = Random.Range(EnemyDir.East.x, max + EnemyDir.East.x);

                float y = Random.Range(min, max);
            }
            if (Direction == EnemyDir.West)
            {
                float x = Random.Range(EnemyDir.West.x, max - EnemyDir.West.x);
                float y = Random.Range(min, max);

            }
            Vector3 Spawnpoint = new Vector(x, y, 0)
        EnemyList = Instantiate(EnemyVariant, Spawnpoint)
        EnemyList[i].InitializeEnemy(Enum Direction, Enum EnemyVariant)
        }
    }
}
