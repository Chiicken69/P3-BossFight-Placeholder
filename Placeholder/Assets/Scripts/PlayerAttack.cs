using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackDamage = 10;
    private float attackTime = 5;
    private GameObject bullet;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet,player.transform);
        }
    }

    }
