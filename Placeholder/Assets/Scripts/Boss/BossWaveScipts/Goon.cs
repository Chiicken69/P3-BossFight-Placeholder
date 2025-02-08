using System;
using UnityEngine;

public class Goon : MonoBehaviour
{
    class EnemyVariant { public int speed; public int hp; public int damage;};

    int _SpeedSet;

    int _hpSet;

    int _damageSet;

    public void InitializeEnemy(Vector2 Direction, int EnemyVariant)
    {
        EnemyVariant goon = new EnemyVariant();
        goon.speed =  1;

        _SpeedSet = goon.speed;


        _hpSet = goon.hp;


        _damageSet = goon.damage;






    }
}
