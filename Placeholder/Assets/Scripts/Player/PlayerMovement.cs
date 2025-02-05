using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]

    [SerializeField] private float _baseMoveSpeed;
    [SerializeField] private float _moveDashSpeed;

    [Header("Cooldowns and timings")]

    [SerializeField] private float _dashCooldownTime;
    public bool isInvincible;
    [SerializeField] private uint _invincibleSeconds;


    [Header("Misc")]


    private float _xInput;
    private float _yInput;
    private Vector2 _baseMoveDir;
    private float DashInput;
    private Rigidbody2D _rb;
   
   



   // Timers
   [SerializeField] private float _invincibleTimer;
    [SerializeField] private float _dashTimer;
   


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dashTimer = _dashCooldownTime;
    }


    void Start()
    {
        
    }

    private void GetPlayerInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");
        DashInput = Input.GetAxisRaw("Jump");
    }

    private void UpdatePlayerPos()
    {
        Vector2 _baseMoveDir = new Vector2(_xInput, _yInput);
        print("base move dir" + _baseMoveDir);
        _baseMoveDir.Normalize();
        Vector2 _moveDir = _baseMoveDir;
        _moveDir *= _baseMoveSpeed;
        
        _rb.AddForce(_moveDir);
        
        if (DashInput > 0 && _dashTimer < 0)
        {
            PlayerDash(_moveDir);
            _dashTimer = _dashCooldownTime;
        }
        
    }
    private void PlayerDash(Vector2 baseMoveDir)
    {
        
        isInvincible = true;
        _rb.AddForce(baseMoveDir * _moveDashSpeed, ForceMode2D.Impulse);
        
    }

    private void RunTimers()
    {
        _dashTimer -= Time.deltaTime;
        if (isInvincible)
        {
            _invincibleTimer += Time.deltaTime;
        }
        if (_invincibleTimer >= _invincibleSeconds)
        {
            isInvincible = false;
            _invincibleTimer = _invincibleSeconds;
        }
       
    }


    // Update is called once per frame
    void Update()
    {   
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        UpdatePlayerPos();
        RunTimers();

        
    }
}
