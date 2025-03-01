using System;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    private GameObject camObject;
    [SerializeField] private GameObject Player;

    private void Awake()
    {
        camObject = this.gameObject;
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        camObject.transform.position = new Vector3(camObject.transform.position.x, Player.transform.position.y, camObject.transform.position.z);
    }
}
